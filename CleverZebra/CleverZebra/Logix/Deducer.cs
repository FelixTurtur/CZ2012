using CleverZebra.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Logix
{
    public class Deducer
    {
        private RelationFactory relationBuilder;
        private List<Line> lines;
        private int puzzleHeight;
        private int puzzleWidth;

        public Deducer(int x, int y) {
            this.puzzleHeight = x;
            this.puzzleWidth = y;
            this.relationBuilder = RelationFactory.getInstance();
            this.lines = new List<Line>();
            char c = 'A';
            for (int i = 0; i < puzzleHeight; i++, c++) {
                this.lines.Add(new Line(c, puzzleWidth));
            }
        }

        public Line getLineFromIdentifier(char ident) {
            foreach (Line l in this.lines) {
                if (l.identifier == ident) { return l; }
            }
            throw new ArgumentException("No Line found for identifier: " + ident);
        }

        public Relation[] considerRelationToLine(Relation r, Line l) {
            if (r.getBaseItem(l.identifier) == null) {
                //cannot use this relation
                return new Relation[] {r};
            }
            if (!r.isRelative()) {
                //direct relation
                //if either side is this line's category, add.
                Line.Rows row = r.isPositive() ? Line.Rows.Positives : Line.Rows.Negatives;
                l.addRelation(r.getBaseItem(l.identifier), r.getRelatedItem(l.identifier), row);
                addInverse(r.getRelatedItem(l.identifier), r.getBaseItem(l.identifier), row);
                return null;
            }
            //relative relation
            string[] items = { r.getBaseItem(l.identifier, Relations.Sides.Left), r.getBaseItem(l.identifier, Relations.Sides.Right) };
            string leftMatch = l.checkForMatch(items[0]);
            string rightMatch = l.checkForMatch(items[1]);
            if (!string.IsNullOrEmpty(leftMatch) && !string.IsNullOrEmpty(rightMatch)) {
                //both sides already matched 
                return null;
            } 
            else if (!string.IsNullOrEmpty(leftMatch) || !string.IsNullOrEmpty(rightMatch)) {
                //if either of the two items has a value, then something can be learnt for the other, if not a complete match
                if (Representation.Relations.isQuantified(r.getRule())) {
                    string unknownItem = leftMatch == null ? items[0] : items[1];
                    object knownValue = l.retrieveValue(leftMatch ?? rightMatch);
                    bool inverse = leftMatch == null ? true : false;
                    string targetItem = l.findTarget(knownValue, Relations.comparativeAmount(r.getRule(), inverse));
                    l.addRelation(targetItem, unknownItem , r.isPositive() ? Line.Rows.Positives : Line.Rows.Negatives);
                    addInverse(unknownItem, targetItem, r.isPositive() ? Line.Rows.Positives : Line.Rows.Negatives);
                }
            } 
            else if (eitherSideMatches(r, l, Line.Rows.Negatives)) {
            }
            return null;
        }

        private void addInverse(string p1, string p2, Line.Rows row) {
            getLineFromIdentifier(p1[0]).addRelation(p1, p2, row);
        }

        private static bool eitherSideMatches(Relation r, Line l, Line.Rows row) {
            string leftMatch = l.checkForMatch(r.getBaseItem(l.identifier, Relations.Sides.Left), row);
            string rightMatch = l.checkForMatch(r.getBaseItem(l.identifier, Relations.Sides.Right), row);
            return !string.IsNullOrEmpty(leftMatch) || !string.IsNullOrEmpty(rightMatch);
        }

        public List<Line> getLineCollection() {
            return lines;
        }

    }
}
