using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
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
            foreach (string s in items) {
                if (string.IsNullOrEmpty(result)) {
                    result += ",";
                }
                result += s;
            }
            return result;
        }

        internal void Clear() {
            this.items = new string[size];
        }

        internal void Add(string item) {
            if (isEmpty()) {
                items[0] = item;
            }
            else {
                if (string.IsNullOrEmpty(items[1])) {
                    items[1] = item;
                }
                else if (string.IsNullOrEmpty(items[2])) {
                    items[2] = item;
                }
                else {
                    throw new ArgumentException("Parsing buffer is full; cannot add " + item + " to " + this.ToString());
                }
            }
        }

        internal bool isEmpty() {
            return items == null || string.IsNullOrEmpty(items[0]);
        }

        internal bool Contains(string p) {
            foreach (string item in items) {
                if (item == p)
                    return true;
            }
            return false;
        }

        internal void dropNonTermTags() {
            for (int i = 0; i < items.Count(); i++) {
                string newItem = "";
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
}
