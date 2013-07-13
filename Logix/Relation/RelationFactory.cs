using Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix
{
    public class RelationFactory
    {
        static RelationFactory instance = null;

        private RelationFactory() { }

        public static RelationFactory getInstance() {
            if (instance == null) {
                instance = new RelationFactory();
            }
            return instance;
        }

        public Relation createRelation(string input) {
            if (Relations.isConditional(input)) {
                return new ConditionalRelation(input);
            }
            if (Relations.isTripleRelative(input)) {
                return new TripleRelativeRelation(input);
            }
            if (Relations.isRelative(input)) {
                return new RelativeRelation(input); 
            }
            return new DirectRelation(input);
        }


        internal Relation createRelation(string left, string right, bool isPositive) {
            return createRelation(left + (isPositive ? Relations.Positive : Relations.Negative) + right);
        }
    }
}
