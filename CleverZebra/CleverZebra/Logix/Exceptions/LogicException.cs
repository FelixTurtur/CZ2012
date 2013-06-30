using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Logix
{
    public class LogicException : ApplicationException
    {
        private string p;

        public LogicException(string message) 
            : base(message) {
            this.p = message;
        }
    }
}
