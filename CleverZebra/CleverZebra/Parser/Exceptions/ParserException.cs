using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra.Parser {
    public class ParserException : ApplicationException {

        public ParserException(string message, Exception e) 
            : base(message) { }

    }
}
