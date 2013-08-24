using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix
{
    class SemanticRelation: Relation
    {
        private string description;

        public SemanticRelation(string input) : base(input) {
            base.items = separateItems(input);
            this.description = input.Substring(input.IndexOf(')') + 1);
        }

        internal override List<string> separateItems(string input) {
            return new List<string> { input.Substring(0, input.IndexOf('(')), input.Substring(input.IndexOf('(') + 1, 1) };
        }

        public override string getRelatedItem(char identifier) {
            return base.getRelatedItem(identifier);
        }
    }
}
