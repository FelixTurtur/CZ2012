using Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix
{
    public abstract class Relation : IComparable<Relation>
    {
        protected string rule;
        protected List<string> items;

        public Relation(String input) {
            this.rule = input;
        }

        public virtual string getRule() {
            return this.rule;
        }

        public virtual string getFlippedRule() {
            if (this.GetType().Name != "DirectRelation") {
                return getRule();
            }
            return this.getRightItem() + this.getEqualityTerm() + this.getLeftItem();
        }

        private string getEqualityTerm() {
            return Relations.getEqualityTerm(getRule());
        }

        public int CompareTo(Relation r2) {
            return this.rule.CompareTo(r2.rule);
        }

        public bool isDirect() {
            return this.GetType().Name == "DirectRelation";
        }
        
        public bool isRelative() {
            return this.GetType().Name == "RelativeRelation";
        }

        public bool isConditional() {
            return this.GetType().Name == "ConditionalRelation";
        }
        
        public bool isPositive() {
            return !this.rule.Contains(Representation.Relations.Negative);
        }

        public bool isSemantic() {
            return this.GetType().Name == "SemanticRelation";
        }

        public string getBaseItem(char identifier, Relations.Sides side = Relations.Sides.Related) {
            if (this.rule.Contains(identifier) == false) {
                return null;
            }
            if (isConditional()) {
                return null;
            }
            if (items.Count > 2) {
                if (side == Relations.Sides.Related) { 
                    //If default value then this wasn't called for a Relative Relation.
                    throw new InconclusiveException("Relation has more items than expected. Cannot identify base item", identifier, this.rule); 
                }
                return items[(int)Relations.Sides.Related] == identifier.ToString() ? items[(int)side] : null;
            }
            if (items.Count < 2) {
                throw new NullReferenceException("Relation Items have not been initialised.");
            }
            return items[0][0] == identifier ? items[0] : items[1];
        }

        public virtual string getRelatedItem(char identifier) {
            if (this.rule.Contains(identifier) == false) {
                return null;
            }
            if (isConditional()) {
                return null;
            }
            return items[0][0] == identifier ? items[1] : items[0];
        }

        public string getLeftItem() {
            if (!isDirect()) return null;
            if (rule.Contains(Relations.Negative)) {
                return rule.Substring(0, rule.IndexOf(Relations.Negative));
            }
            else {
                return rule.Substring(0, rule.IndexOf(Relations.Positive));
            }
        }

        public string getRightItem() {
            if (!isDirect()) return null;
            if (rule.Contains(Relations.Negative)) {
                return rule.Substring(rule.IndexOf(Relations.Negative) + 2);
            }
            else {
                return rule.Substring(rule.IndexOf(Relations.Positive) + 1);
            }
        }

        internal abstract List<string> separateItems(string input);
        
        internal string getComparator() {
            return Relations.getComparator(rule);
        }

        internal string getDescriber() {
            if (!isSemantic()) return null;
            return Relations.getDescriber(rule);
        }

    }
}
