using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Representation;

namespace Logix
{
    class RelativeRelation : Relation
    {
        public RelativeRelation(string input) : base(input){
            base.items = separateItems(input);
        }

        internal override List<string> separateItems(string input) {
            List<string> items = new List<string>();
            string item = "";
            for (int i = 0; i < input.Length; i++) {
                if (Relations.isPossessive(input[i]) || Relations.isComparator(input[i]) || Relations.isEqualityTerm(input[i])) {
                    if (!string.IsNullOrEmpty(item)) {
                        items.Add(item);
                        item = "";
                    }
                } else {
                    item += input[i];
                    //final "item" in a quantified Relative (i.e. the difference) will not be added to items.
                }
            }
            return items;
        }

        public new bool isPositive() {
            return !rule.Contains(Representation.Relations.Negative) && rule.Contains(Representation.Relations.Positive);
        }

        public override string getRelatedItem(char identifier) {
            if (this.rule.Contains(identifier) == false) {
                return null;
            }
            if (items.Count > 2) {
                return items[(int)Relations.Sides.Related];
            }
            return items[0][0] == identifier ? items[1] : items[0];
        }
    }
}
