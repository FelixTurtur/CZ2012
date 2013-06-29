using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra.Logix
{
    class DirectRelation : Relation
    {
        public DirectRelation(string input) : base(input) {
            base.items = separateItems(input);
        }

        private List<string> separateItems(string input) {
            List<string> items = new List<string>();
            string item = "";
            for (int i = 0; i < input.Length; i++) {
                if (Representation.Relations.isEqualityChar(input[i])) {
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
