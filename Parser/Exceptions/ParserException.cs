using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser {
    public class ParserException : ApplicationException {

        public ParserException(string message, Exception e) 
            : base(message, e) { }

        public ParserException(string message) : base(message) { }
    }
}
