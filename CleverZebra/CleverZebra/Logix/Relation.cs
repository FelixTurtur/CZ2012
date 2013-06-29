using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Logix
{
    public class Relation : IComparable<Relation>
    {
        public string rule {get; private set;}
        public bool isRelative;
        private List<string> items;

        public Relation(String input) {
            this.rule = input;
            this.items = separateItems(input);
        }

        private List<string> separateItems(string input) {
            List<string> items = new List<string>();
            string item = "";
            for (int i = 0; i < input.Length; i++) {
                if (input[i] == ' ') {
                    if (!string.IsNullOrEmpty(item)) {
                        items.Add(item);
                    }
                } else {

                }
            }
            return items;
        }

        public int CompareTo(Relation r2) {
            return this.rule.CompareTo(r2.rule);
        }

        public bool isPositive() {
            return !isRelative && !this.rule.Contains(Representation.Relations.Negative);
        }


        internal string getBaseItem(string identifier) {
            throw new NotImplementedException();   
        }

        internal string getRelatedItem(string identifier) {
            throw new NotImplementedException();
        }
    }
}
