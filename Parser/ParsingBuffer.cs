using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZParser
{
    internal class ParsingBuffer
    {
        private string[] items;
        private int size;

        internal ParsingBuffer(int n) {
            size = n;
            items = new string[size];
        }

        public override string ToString() {
            string result = "";
            foreach (string item in items) {
                if (string.IsNullOrEmpty(item)) {
                    continue;
                }
                if (!string.IsNullOrEmpty(result)) {
                    result += ",";
                }
                result += item;
            }
            return result;
        }

        internal void Clear() {
            this.items = new string[size];
        }

        internal void Add(string item) {
            int index = firstNonEmpty();
            if (index == -1) {
                throw new ArgumentException("Parsing buffer is full; cannot add " + item + " to " + this.ToString());
            }
            items[index] = item;
        }

        private int firstNonEmpty() {
            for (int i = 0; i < this.size; i++) {
                if (string.IsNullOrEmpty(items[i])) {
                    return i;
                }
            }
            return -1;
        }

        internal bool isEmpty() {
            return items == null || string.IsNullOrEmpty(items[0]);
        }

        internal bool Contains(string p) {
            foreach (string item in items) {
                if (item == null) {
                    return false;
                }
                if (item == p)
                    return true;
                else {
                    foreach (string bit in item.Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                        if (bit == p)
                            return true;
                    }
                }
            }
            return false;
        }

        internal void dropNonTermTags() {
            for (int i = 0; i < items.Count(); i++) {
                string newItem = "";
                if (items[i] != null) {
                    string[] bits = items[i].Split(new char[] { ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string b in bits) {
                        if (b[0] == 'T') {
                            //never more than one TermTags in a tag.
                            newItem = b;
                        }
                    }
                    items[i] = newItem;
                }
            }
        }

        internal bool hasCombinedCats() {
            if (isEmpty()) {
                return false;
            }
            return !this.ToString().Contains("T");
        }

        internal bool hasMixedTags() {
            if (isEmpty()) {
                return false;
            }
            if (!this.ToString().Contains("T")) {
                return false;
            }
            string[] bits = this.ToString().Split(new char[] { ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string t in bits) {
                if (Tagger.isCatTag(t)) {
                    return true;
                }
            }
            return false;
        }

        internal bool stretchesOverWhitespace() {
            //Buffers of Combined cat tags ("B2,B") do not stretch
            //Buffers of Term tags (other than To) do stretch
            return !this.hasCombinedCats();
        }

        internal string pullCategoryTitle() {
            if (string.IsNullOrEmpty(items[1]) && !string.IsNullOrEmpty(items[0])) {
                //if we've used more than one buffer space, cannot assume mention of category title
                foreach (string bit in items[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                    if (Tagger.isCatTag(bit) && bit.Length == 1) {
                        return bit;
                    }
                }
            }
            return null;
        }
    }
}
