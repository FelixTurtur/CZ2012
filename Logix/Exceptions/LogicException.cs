using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix {
    public class LogicException : ApplicationException {
        private string p;
        private Exception e;
        public string problemItem;

        public LogicException(string message, Exception e)
            : base(message, e) {
            this.p = message;
            this.e = e;
        }

        public LogicException(string message, string item)
            : base(message) {
            this.problemItem = item;
            this.p = message;
        }
    }
}
