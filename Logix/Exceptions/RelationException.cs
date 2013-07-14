using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix
{
    class RelationException : ApplicationException
    {
        private string p;
        private string rule;

        public RelationException(string p, string rule) {
            this.p = p;
            this.rule = rule;
        }

    }
}
