using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix
{
    class DirectRelation : Relation
    {
        public DirectRelation(string input) : base(input) {
            base.items = separateItems(input);
        }

        internal override List<string> separateItems(string input) {
            List<string> items = new List<string>();
            string item = "";
            for (int i = 0; i <= input.Length; i++) {
                if (i == input.Length && !string.IsNullOrEmpty(item)) {
                    items.Add(item);
                }
                else if (Representation.Relations.isEqualityTerm(input[i])) {
                    if (!string.IsNullOrEmpty(item)) {
                        items.Add(item);
                        item = "";
                    }
                } else {
                    item += input[i];
                }
            }
            return items;
        }

        public override string getRelatedItem(char identifier) {
            if (this.rule.Contains(identifier) == false) {
                return null;
            }
            if (items.Count > 2) {
                throw new InconclusiveException("More items than expected in Direct Relation");
            }
            return items[0][0] == identifier ? items[1] : items[0];
        }
    }
}
