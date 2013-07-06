using System;
using Representation;


namespace Logix {
    class InconclusiveException : ApplicationException {
        private string p;
        private string rule;
        private char identifierChecked;

        public InconclusiveException(Puzzle p) {
            this.Data.Add("puzzle", p);
        }

        public InconclusiveException(char ident, string rule) {
            // TODO: Complete member initialization
            this.p = "Relation has more items than expected. Cannot identify required item";
            this.rule = rule;
            this.identifierChecked = ident;
            this.Data.Add("Identifier", ident);
            this.Data.Add("Rule", rule);
        }

        public InconclusiveException(string message)
            : base(message) {
            this.p = message;
        }
    }
}
