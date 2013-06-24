using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleverZebra.Logix
{
    public class Line
    {
        public char identifier {get; set;}
        public int size {get; set;}
        private string[] rules;

        public Line() { size = 0; identifier = 'Z'; }
        public Line(char ident, int size)
        {
            identifier = ident;
            this.size = size;
        }
        public Line(char ident, int size, string[] rules)
        {
            identifier = ident;
            this.size = size;
            this.rules = rules;
        }

        //public int getSize()
        //{
        //    return size;
        //}

    }
}
