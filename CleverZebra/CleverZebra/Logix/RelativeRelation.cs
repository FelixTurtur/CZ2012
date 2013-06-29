using CleverZebra.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra.Logix
{
    class RelativeRelation : Relation
    {
        public RelativeRelation(string input) : base(input){
            base.items = separateItems(input);
        }

        private List<string> separateItems(string input) {
            List<string> items = new List<string>();
            string item = "";
            for (int i = 0; i < input.Length; i++) {
                if (Relations.isPossessive(input[i]) || Relations.isComparator(input[i])) {
                    if (!string.IsNullOrEmpty(item)) {
                        items.Add(item);
                        item = "";
                    }
                } else if (Relations.isEqualityChar(input[i])) {
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

    }
}
