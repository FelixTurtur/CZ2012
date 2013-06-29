using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra.Logix
{
    class InconclusiveException : Exception
    {
        private string p;
        private string rule;
        private string identifierChecked;

        public InconclusiveException(string ident, string rule) {
            // TODO: Complete member initialization
            this.p = "Relation has more items than expected. Cannot identify required item";
            this.rule = rule;
            this.identifierChecked = ident;
        }
    }
}
