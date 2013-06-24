using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleverZebra.Logix
{
    /* Line class 
     * Creates a matrix that will store information on one category. The matrix is always three rows deep with one labelling column
     * and then one column per value within the category. The top row holds the index, the second the value (if necessary) and the third
     * any known relational data so that logical deductions can be drawn. 
     * Rules keeps a copy of keywords relevant to the category.
     * The Identifier is the [A|B|C|D|E...] puzzle-scope char for this category.
     * */
    public class Line
    {
        public char identifier {get; private set;}
        public int size {get; private set;}
        private string[] rules;
        private object[][] innerArray;

        public Line() { size = 0; identifier = 'Z'; }
        public Line(char ident, int size)
        {
            identifier = ident;
            this.size = size;
            this.innerArray = createArray(ident, size);
        }

        public Line(char ident, int size, string[] rules)
        {
            identifier = ident;
            this.size = size;
            this.rules = rules;
            this.innerArray = createArray(ident, size);
        }

        private object[][] createArray(char ident, int size) {
            object[][] newArray = { new object[size + 1], new object[size + 1], new object[size + 1] };
            newArray[1][0] = ident;
            for (int i = 0; i <= size; i++) {
                newArray[0][i] = i;
            }
            return newArray; 
        }


        public void enterValues(object[] list) {
            if (list.Length > this.size) {
                throw new ArgumentException("List contains too many arguements for this Line. Line: " + this.identifier + " Size: " + this.size + " List length: " + list.Length);
            }
            if (list.Length < this.size) {
                throw new ArgumentException("List contains too few arguements for this Line. Line: " + this.identifier + " Size: " + this.size + " List length: " + list.Length);
            }
            for (int i = 1; i <= size; i++) {
                this.innerArray[1][i] = list[i - 1];
            }
        }

        public object retrieveValue(string index) {
            if (index.Substring(0, 1) != this.identifier.ToString()) {
                throw new ArgumentException("Identifier does not match Index provided: " + index);
            }
            int i = Convert.ToInt32(index.Substring(1));
            if (size < i) {
                throw new IndexOutOfRangeException("Index not within Line: " + this.identifier);
            }
            return this.innerArray[1][i];
        }
    }
}
