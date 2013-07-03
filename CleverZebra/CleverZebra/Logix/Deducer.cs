using CleverZebra.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Logix {
    public class Deducer {
        private RelationFactory relationBuilder;
        private List<Category> cats;
        private int puzzleHeight;
        private int puzzleWidth;

        public Deducer(int x, int y) {
            this.puzzleHeight = x;
            this.puzzleWidth = y;
            this.relationBuilder = RelationFactory.getInstance();
            this.cats = new List<Category>();
            char c = 'A';
            for (int i = 0; i < puzzleHeight; i++, c++) {
                this.cats.Add(new Category(c, puzzleWidth));
            }
        }

        public Category getCategoryFromIdentifier(char ident) {
            foreach (Category l in this.cats) {
                if (l.identifier == ident) { return l; }
            }
            throw new ArgumentException("No Category found for identifier: " + ident);
        }

        public Relation[] considerRelationToCategory(Relation r, Category cat) {
            if (!r.getRule().Contains(cat.identifier)) {
                //cannot use this relation
                return new Relation[] { r };
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
                if (Representation.Relations.isQuantified(r.getRule())) {
                    string unknownItem = leftMatch == null ? items[0] : items[1];
                    object knownValue = cat.retrieveValue(leftMatch ?? rightMatch);
                    bool inverse = leftMatch == null ? true : false;
                    string targetItem = cat.findTarget(knownValue, Relations.comparativeAmount(r.getRule(), inverse));
                    cat.addRelation(targetItem, unknownItem, r.isPositive() ? Category.Rows.Positives : Category.Rows.Negatives);
                    addInverse(unknownItem, targetItem, r.isPositive() ? Category.Rows.Positives : Category.Rows.Negatives);
                    return null;
                }
                else {
                    Relation[] results = cat.considerComparative(leftMatch ?? items[0], Relations.getComparator(r.getRule()), rightMatch ?? items[1]);
                }
            }
            return new Relation[] { r };
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

        internal void setRules(List<string> rules) {
            throw new NotImplementedException();
        }

        internal CleverZebra.Solution go() {
            throw new NotImplementedException();
        }
    }
}
