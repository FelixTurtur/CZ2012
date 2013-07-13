using Representation;
using System.Collections.Generic;
using Logix;

namespace Logix
{
    class TripleRelativeRelation : Relation
    {
        public TripleRelativeRelation(string input) : base(input) {
            base.items = separateItems(input);
        }

        internal override List<string> separateItems(string input) {
            if (Representation.Relations.isQuantified(input)) {
                throw new RelationException("Cannot create Triple Relation with an equality operator", input);
            }
            List<string> items = new List<string>();
            string item = "";
            for (int i = 0; i < input.Length; i++) {
                if (Relations.isPossessive(input[i]) || Relations.isComparator(input[i])) {
                    if (!string.IsNullOrEmpty(item)) {
                        items.Add(item);
                        item = "";
                    }
                }
                else {
                    item += input[i];
                    //final "item" will be the last possessive-marcated item.
                }
            }
            string relatedCategory = items[1];
            for (int i = 3; i <= items.Count; i += 2) {
                if (items[i] != relatedCategory) {
                    throw new RelationException("Cannot create Triple Relation unless all items relate to the same category", input);
                }
            }
            return items;
        }
    }
}
