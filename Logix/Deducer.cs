using Representation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logix {
    public class Deducer {
        private RelationFactory relationBuilder;
        private List<Category> cats;
        private string[] keywords;
        private List<string> clues;
        private List<string> usedClues;
        private int puzzleBreadth;
        private int puzzleDepth;
        private Solution solution;
        private const int MAXTURNS = 200;
        private const int REDUCTIO_SPACING = 8;

        public Deducer(int x, int y, string[] keys = null) {
            this.puzzleBreadth = x;
            this.puzzleDepth = y;
            this.relationBuilder = RelationFactory.getInstance();
            this.cats = new List<Category>();
            this.keywords = keys ?? new string[4];
            CategoryBuilder catBuilder = new CategoryBuilder();
            char c = 'A';
            for (int i = 0; i < puzzleBreadth; i++, c++) {
                catBuilder.newKitty();
                catBuilder.setIdentifier(c);
                catBuilder.setSize(puzzleDepth);
                if (!string.IsNullOrEmpty(keywords[i])) {
                    catBuilder.setKeyword(keywords[i]);
                }
                this.cats.Add(catBuilder.build());
            }
            solution = new Solution(x,y);
        }

        public Category getCategoryFromIdentifier(char ident) {
            foreach (Category l in this.cats) {
                if (l.identifier == ident) { return l; }
            }
            throw new ArgumentException("No Category found for identifier: " + ident);
        }

        public List<Relation> considerRelationToCategory(Relation r, Category cat) {
            if (!r.getRule().Contains(cat.identifier)) {
                //cannot use this relation
                return new List<Relation> {r};
            }
            if (!r.isRelative()) {
                //direct relation
                //if either side is this category, add.
                Category.Rows row = r.isPositive() ? Category.Rows.Positives : Category.Rows.Negatives;
                cat.addRelation(r.getBaseItem(cat.identifier), r.getRelatedItem(cat.identifier), row);
                addInverse(r.getRelatedItem(cat.identifier), r.getBaseItem(cat.identifier), row);
                return null;
            }
            //relative relation
            string[] items = { r.getBaseItem(cat.identifier, Relations.Sides.Left), r.getBaseItem(cat.identifier, Relations.Sides.Right) };
            string leftMatch = cat.checkForMatch(items[0]);
            string rightMatch = cat.checkForMatch(items[1]);
            if (!string.IsNullOrEmpty(leftMatch) && !string.IsNullOrEmpty(rightMatch)) {
                //both sides already matched 
                return null;
            }
            else if (!string.IsNullOrEmpty(leftMatch) || !string.IsNullOrEmpty(rightMatch)) {
                //if either of the two items has a value, then something can be learnt for the other, if not a complete match
                List<Relation> results = new List<Relation>();
                if (Representation.Relations.isQuantified(r.getRule())) {
                    string unknownItem = leftMatch == null ? items[0] : items[1];
                    object knownValue = cat.retrieveValue(leftMatch ?? rightMatch);
                    bool inverse = leftMatch == null ? true : false;
                    string targetItem = cat.findTarget(knownValue, Relations.comparativeAmount(r.getRule(), inverse));
                    cat.addRelation(targetItem, unknownItem, r.isPositive() ? Category.Rows.Positives : Category.Rows.Negatives);
                    addInverse(unknownItem, targetItem, r.isPositive() ? Category.Rows.Positives : Category.Rows.Negatives);
                    results.Add(relationBuilder.createRelation(targetItem, unknownItem, r.isPositive()));
                    return results;
                }
                else {
                    results = cat.considerComparative(leftMatch ?? items[0], Relations.getComparator(r.getRule()), rightMatch ?? items[1]);
                }
            }
            else {
                if (items[0] != null && items[1] != null) {
                    if (usedClues.Contains(r.getRule())) {
                        //Nothing new learnt
                        return new List<Relation> {r};
                    }
                    //create negative relations from comparatives
                    List<Relation> relations = new List<Relation>();
                    relations = (createNegativeRelationsToBounds(items[0], items[1], r.getComparator(), cat.identifier, cat.size));
                    if (relations == null) relations = new List<Relation> { r };
                    else relations.Add(r);
                    return relations;

                }
            }
            return new List<Relation> { r };
        }

        private List<Relation> createNegativeRelationsToBounds(string leftItem, string rightItem, string comparator, char ident, int size) {
            List<Relation> relations = new List<Relation>();
            string lowestInCat = ident + "1";
            string highestInCat = ident + size.ToString();
            if (Relations.checkDirection(comparator) == Relations.Directions.Higher) {
                //left cannot be lowest; right cannot be highest
                relations.Add(relationBuilder.createRelation(leftItem, lowestInCat, false));
                relations.Add(relationBuilder.createRelation(rightItem, highestInCat, false));
            }
            else if (Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                relations.Add(relationBuilder.createRelation(leftItem, highestInCat, false));
                relations.Add(relationBuilder.createRelation(rightItem, lowestInCat, false));

            }
            return relations;
        }

        private void addInverse(string p1, string p2, Category.Rows row) {
            getCategoryFromIdentifier(p1[0]).addRelation(p1, p2, row);
        }

        private static bool eitherSideMatches(Relation r, Category cat) {
            string leftMatch = cat.checkForMatch(r.getBaseItem(cat.identifier, Relations.Sides.Left));
            string rightMatch = cat.checkForMatch(r.getBaseItem(cat.identifier, Relations.Sides.Right));
            return !string.IsNullOrEmpty(leftMatch) || !string.IsNullOrEmpty(rightMatch);
        }

        public List<Category> getCategoryCollection() {
            return cats;
        }

        internal void setClues(List<string> clues) {
            this.clues = clues;
        }

        internal int[,] go() {
            int turn = 1;
            usedClues = new List<string>();
            while (clues.Count() > 0 && turn < MAXTURNS && !solution.isComplete()) {
                string clue = clues[0];
                List<Relation> relations = considerRelationToCategories(relationBuilder.createRelation(clue));
                List<Relation> relations2 = solution.considerRelationInSolution(relationBuilder.createRelation(clue));
                List<Relation> relations3 = solution.checkAllButOnes();
                combineRelationRanges(ref relations, relations2);
                combineRelationRanges(ref relations, relations3);
                usedClues.Add(clues[0]);
                System.Diagnostics.Debug.WriteLine("Used clue: " + clues[0]);
                clues.RemoveAt(0);
                //if (relations != null && relations.Count() ==1 && relations[0].getRule() == clue) {
                    //Nothing new learnt
                if (turn % REDUCTIO_SPACING == 0) {
                    relations.AddRange(reductio());
                }
                if (relations != null && relations.Count() > 0) {
                    foreach (Relation r in relations) {
                        addRelation(r);
                    }
                }
                turn++;
            }
            return solution.getFinalMatrix();
        }

        /// <summary>
        /// Performs a check for any other relations that can be deduced by considering related items' pairings.
        /// </summary>
        /// <returns>A list of relations discovered.</returns>
        private List<Relation> reductio() {
            List<Relation> results = new List<Relation>();
            bool fullyChecked = false;
            while (!fullyChecked) {
                foreach (Category cat in cats) {
                    List<Relation> deductedRelations = cat.checkDeductibles();
                    if (deductedRelations.Count() > 0) {
                        results.AddRange(deductedRelations);
                    }
                    for (int itemIndex = 1; itemIndex <= cat.size; itemIndex++) {
                        string[] matchedItems = Category.getMatchedItems(cat, itemIndex);
                        if (matchedItems != null) {
                            string[] unmatchedItems = Category.getUnmatchedItems(cat, itemIndex);
                            foreach (string match in matchedItems) {
                                //see if matched item has negative connections to items first item doesn't
                                string[] relatedUnmatches = Category.getUnmatchedItems(getCategoryFromIdentifier(match[0]), Convert.ToInt32(match[1].ToString()));
                                if (relatedUnmatches == null) continue;
                                string[] newNegatives = relatedUnmatches.Except(unmatchedItems ?? new string[] {""}).ToArray();
                                if (newNegatives.Count() > 0) {
                                    string comparedItem = cat.identifier.ToString() + itemIndex;
                                    combineRelationRanges(ref results, createNegativeRelations(comparedItem, newNegatives));
                                }
                            }
                        }
                        List<Relation> allButOnes = checkNegativesForAllButOne(cat, itemIndex);
                        if (allButOnes != null) {
                            combineRelationRanges(ref results, allButOnes);
                        }
                    }
                }
                fullyChecked = true;
            }
            return results;
        }

        /// <summary>
        /// Returns a relation per category if only one valid option remains.
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        private List<Relation> checkNegativesForAllButOne(Category cat, int itemIndex) {
            string[] Negatives = Category.getUnmatchedItems(cat, itemIndex);
            if (Negatives == null || Negatives.Count() < cat.size-1) {
                return null;
            }
            char ident = 'A';
            for (int catIndex = 0; catIndex < cat.size; catIndex++, ident++) {
                if (Negatives.Where(a => a.StartsWith(ident.ToString())).Count() == cat.size - 1) {
                    //All but one negated. Find positive
                    string positiveMatchItem = "";
                    for (int i = 1; i <= cat.size; i++) {
                        if (!Negatives.Contains(ident.ToString() + i)) {
                            positiveMatchItem = ident.ToString() + i;
                        }
                    }
                    addRelation(relationBuilder.createRelation(cat.identifier.ToString() + itemIndex, positiveMatchItem, true));
                    addInverse(cat.identifier.ToString() + itemIndex, positiveMatchItem, Category.Rows.Positives);
                }
            }
            return null;
        }

        /// <summary>
        /// Creates negative relations for the item against a list of related items
        /// </summary>
        /// <param name="item"></param>
        /// <param name="notRelateds"></param>
        /// <returns></returns>
        private List<Relation> createNegativeRelations(string item, string[] notRelateds) {
            List<Relation> relations = new List<Relation>();
            foreach (string s in notRelateds) {
                if (item[0] == s[0]) {
                    continue;
                }
                relations.Add(relationBuilder.createRelation(item, s, false));
            }
            return relations;
        }

        /// <summary>
        /// Checks a Relation against each Category
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        private List<Relation> considerRelationToCategories(Relation relation) {
            List<Relation> result = new List<Relation>();
            foreach (Category cat in cats) {
                combineRelationRanges(ref result, considerRelationToCategory(relation, cat));
            }
            return result;
        }

        /// <summary>
        /// Updates one set of Relations with all distinct Relations from another
        /// </summary>
        /// <param name="relations1"></param>
        /// <param name="relations2"></param>
        private void combineRelationRanges(ref List<Relation> relations1, List<Relation> relations2) {
            if (relations1 == null && relations2 == null) {
                return;
            }
            if (relations1 == null) {
                relations1 = relations2.Distinct<Relation>().ToList();
            }
            if (relations2 == null) {
                return;
            }
            //combine unique
            foreach (Relation r in relations2) {
                if (relations1.Any(a => a.CompareTo(r)==0)) { continue; }
                relations1.Add(r);
            }
            relations1 = relations1.Distinct<Relation>().ToList();
        }

        internal object getRemainingClueCount() {
            return this.clues.Count;
        }

        /// <summary>
        /// Adds Relation to clue bank if not seen already, or if it is Relative
        /// </summary>
        /// <param name="r"></param>
        internal void addRelation(Relation r) {
            if (clues.Contains(r.getRule())) return;
            if (usedClues.Contains(r.getRule()) && usedClues.Count() > 10 && !r.isRelative()) return;
            this.clues.Add(r.getRule());
        }

        internal void enterCategoryValues(char ident, object[] vals) {
            getCategoryFromIdentifier(ident).enterValues(vals);
        }
    }
}
