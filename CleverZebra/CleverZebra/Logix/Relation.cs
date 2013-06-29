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

        internal string getBaseItem(string identifier) {
            throw new NotImplementedException();   
        }

        internal string getRelatedItem(string identifier) {
            throw new NotImplementedException();
        }
    }
}
