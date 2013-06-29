using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Logix
{
    public abstract class Relation : IComparable<Relation>
    {
        protected string rule;
        protected List<string> items;

        public Relation(String input) {
            this.rule = input;
        }

        public string getRule() {
            return this.rule;
        }

        public int CompareTo(Relation r2) {
            return this.rule.CompareTo(r2.rule);
        }

        public bool isRelative() {
            return this.GetType().Name == "RelativeRelation";
        }
        
        public bool isPositive() {
            return !this.rule.Contains(Representation.Relations.Negative);
        }

        public string getBaseItem(string identifier) {
            if (items.Count > 2) {
                throw new InconclusiveException(identifier, this.rule);
            }
            return items[0].StartsWith(identifier) ? items[0] : items[1];
        }

        public string getRelatedItem(string identifier) {
            if (items.Count > 2) {
                throw new InconclusiveException(identifier, this.rule);
            }
            return items[0].StartsWith(identifier) ? items[1] : items[0];
        }
    }
}
