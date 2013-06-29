using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleverZebra.Logix
{
    /* Line class 
     * Creates a matrix that will store information on one category. The matrix is always four rows deep with one labelling column
     * and then one column per value within the category. The top row holds the index, the second the value (if necessary) and the third
     * any known relational data so that logical deductions can be drawn. 
     * Rules keeps a copy of keywords relevant to the category.
     * The Identifier is the [A|B|C|D|E...] puzzle-scope char for this category.
     * */
    public class Line
    {
        public enum Rows
        {
            Index = 0,
            Values = 1,
            Positives = 2,
            Negatives = 3
        };

        public string identifier {get; private set;}
        public int size {get; private set;}
        private string[] rules;
        private object[][] innerArray;

        public Line() { size = 0; identifier = "Z"; }
        public Line(string ident, int size)
        {
            identifier = ident;
            this.size = size;
            this.innerArray = createArray(ident, size);
        }

        public Line(string ident, int size, string[] rules)
        {
            identifier = ident;
            this.size = size;
            this.rules = rules;
            this.innerArray = createArray(ident, size);
        }

        private object[][] createArray(string ident, int size) {
            object[][] newArray = { new object[size + 1], new object[size + 1], new string[size + 1], new string[size+1] };
            newArray[1][0] = ident;
            for (int i = 0; i <= size; i++) {
                newArray[0][i] = i;
                newArray[2][i] = "";
                newArray[3][i] = "";
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

        public void addRelation(string p1, string p2, Line.Rows row = Line.Rows.Positives) {
            if (p1.Substring(0, 1) != identifier) {
                throw new ArgumentException("Identifier does not match target location: " + p1);
            }
            int column = Convert.ToInt32(p1.Substring(1));
            if (column > size) {
                throw new ArgumentException("Target is out of bounds: " + p1);
            }
            this.innerArray[(int)row][column] += p2;
        }

        public string checkForMatch(string p) {
            int unknowns = 0;
            string category = p.Substring(0, 1);
            string result = null;
            for (int i = 0; i < size && unknowns < 2; i++) {
                if (!this.innerArray[2][i + 1].ToString().Contains(category)) {
                    unknowns++;
                    result = this.identifier + (i+1).ToString();
                }
            }
            return unknowns < 2 ? result : null;
        }

        public bool considerRelation(Relation r) {
            if (!r.isRelative) {
                //direct relation. Chuck it in.
                Line.Rows row = r.isPositive() ? Line.Rows.Positives : Line.Rows.Negatives;
                this.addRelation(r.getBaseItem(identifier), r.getRelatedItem(identifier), row);
                return true;
            }
            //relative relation
            return false; //implement later
        }
    }
}
