using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra.Logix
{
    class InconclusiveException : ApplicationException
    {
        private string p;
        private string rule;
        private char identifierChecked;

        public InconclusiveException(char ident, string rule) {
            // TODO: Complete member initialization
            this.p = "Relation has more items than expected. Cannot identify required item";
            this.rule = rule;
            this.identifierChecked = ident;
        }

        public InconclusiveException(string message)
            : base(message) {
            this.p = message;
        }
    }
}
