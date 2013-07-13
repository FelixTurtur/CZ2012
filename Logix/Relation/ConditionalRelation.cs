using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Representation;

namespace Logix
{
    class ConditionalRelation : Relation
    {
        internal Relation conditional;
        private Relation trueRelation;
        private Relation falseRelation;


        public ConditionalRelation(string input) : base(input) {
            conditional = RelationFactory.getInstance().createRelation(getConditionalStatement());
            trueRelation = RelationFactory.getInstance().createRelation(getIfTrueStatement());
            falseRelation = RelationFactory.getInstance().createRelation(getIfFalseStatement());
            base.items = separateItems(input);
        }

        internal string getIfFalseStatement() {
            return Representation.Relations.getIfFalseStatement(base.rule);
        }

        internal string getIfTrueStatement() {
            return Representation.Relations.getIfTrueStatement(base.rule);
        }

        internal string getConditionalStatement() {
            return Representation.Relations.getConditionalStatement(base.rule);
        }

        internal override List<string> separateItems(string input) {
            if (conditional.isRelative()) {
                return ((RelativeRelation)conditional).separateItems(getRule());
            }
            else
                return ((DirectRelation)conditional).separateItems(getRule());
        }

        public new string getRule() {
            return conditional.getRule();
        }
    }
}
