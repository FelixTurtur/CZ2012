using Representation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Logix {
    public delegate void SolveCompleteHandler(Deducer sender, SolveCompleteArgs e);
    
    public class Deducer
    {
        public event SolveCompleteHandler Concluded;
        private RelationFactory relationBuilder;
        private List<Category> cats;
        private string[] keywords;
        private List<string> clues;
        private List<string> usedClues;
        private int puzzleBreadth;
        private int puzzleDepth;
        private Solution solution;
        private const int MAXTURNS = 200;
        private const int ABSURDIO_SPACING = 8;

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
                cats[i].Matched += Deducer_Matched;
            }
            solution = new Solution(x,y);
        }

        internal void enterCategoryValues(char ident, object[] vals) {
            getCategoryFromIdentifier(ident).enterValues(vals);
        }

        public Category getCategoryFromIdentifier(char ident) {
            foreach (Category l in this.cats) {
                if (l.identifier == ident) { return l; }
            }
            throw new ArgumentException("No Category found for identifier: " + ident);
        }

        void Deducer_Matched(Category sender, MatchEventArgs e) {
           List<Relation> newRules = solution.considerRelationInSolution(relationBuilder.createRelation(e.item1, e.item2, true));
           combineRelationRanges(ref newRules, solution.checkAllButOnes());
           if (newRules.Count > 0) {
              foreach (Relation r in newRules) {
                 addRelationToClues(r);
              }
           }
        }

        private void OnSolutionComplete(SolveCompleteArgs solveCompleteArgs) {
            if (Concluded != null) {
                Concluded(this, solveCompleteArgs);
            }
        }

        public List<Category> getCategoryCollection() {
            return cats;
        }

        internal void setClues(List<string> clues) {
            this.clues = clues;
        }

        internal object getRemainingClueCount() {
            return this.clues.Count;
        }

        /// <summary>
        /// The main high-level algorithm for solving puzzles
        /// </summary>
        /// <returns></returns>
        internal int[,] Go() {
            Stopwatch ticker = new Stopwatch();
            ticker.Start();
            int turn = 1;
            usedClues = new List<string>();
            while (clues.Count() > 0 && turn < MAXTURNS && !solution.isComplete()) {
                string clue = clues[0];
                List<Relation> relations = considerRelationToCategories(relationBuilder.createRelation(clue));
                usedClues.Add(clues[0]);
                clues.RemoveAt(0);
                if (turn % ABSURDIO_SPACING == 0) {
                    relations.AddRange(Absurdio());
                }
                if (relations != null && relations.Count() > 0) {
                    foreach (Relation r in relations) {
                        addRelationToClues(r);
                    }
                }
                turn++;
            }
            ticker.Stop();
            TimeSpan t = ticker.Elapsed;
            OnSolutionComplete(new SolveCompleteArgs(solution.isComplete(), turn, t));
            return solution.getFinalMatrix();
        }

        /// <summary>
        /// Checks a Relation against each Category
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        private List<Relation> considerRelationToCategories(Relation relation) {
            List<Relation> result = new List<Relation>();
            foreach (Category cat in cats) {
               combineRelationRanges(ref result, cat.considerRelationToCategory(relation, usedClues.Contains(relation.getRule())));
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

        /// <summary>
        /// Performs a check for any other relations that can be deduced by considering related items' pairings.
        /// (Reductio ad absurdum)
        /// </summary>
        /// <returns>A list of relations discovered.</returns>
        private List<Relation> Absurdio() {
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
                    addRelationToClues(relationBuilder.createRelation(cat.identifier.ToString() + itemIndex, positiveMatchItem, true));
                    addInverseRelation(cat.identifier.ToString() + itemIndex, positiveMatchItem, Category.Rows.Positives);
                }
            }
            return null;
        }

        /// <summary>
        /// Adds Relation to clue bank if not seen already, or if it is Relative
        /// </summary>
        /// <param name="r"></param>
        internal void addRelationToClues(Relation r) {
            if (clues.Contains(r.getRule())) return;
            if (usedClues.Contains(r.getRule()) && usedClues.Count() > 10 && r.isDirect()) return;
            this.clues.Add(r.getRule());
        }

        private void addInverseRelation(string p1, string p2, Category.Rows row) {
            getCategoryFromIdentifier(p1[0]).addRelation(p1, p2, row);
        }

    }
}
