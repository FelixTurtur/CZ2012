using System;
using System.Linq;
using CleverZebra.Logix.Calculators;

namespace CleverZebra.Logix
{
    /* Line class 
     * Creates a matrix that will store information on one category. The matrix is always four rows deep with one labelling column
     * and then one column per value within the category. The top row holds the index, the second the value (if necessary) and the 
     * third/fourth any known positive/negative relational data so that logical deductions can be drawn. 
     * Rules keeps a copy of keywords relevant to the category.
     * The Identifier is the [A|B|C|D|E...] puzzle-scope character for this category.
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

        public char identifier {get; private set;}
        public int size {get; private set;}
        private string keyword;
        private object[][] innerArray;
        private Calculator calculator;

        public Line() { size = 0; identifier = 'Z'; }
        public Line(char ident, int size)
        {
            identifier = ident;
            this.size = size;
            this.innerArray = createArray(ident, size);
        }

        public Line(char ident, int size, string special)
        {
            identifier = ident;
            this.size = size;
            this.keyword = special;
            calculator = CalculatorFactory.getInstance().createCalculator(special);
            this.innerArray = createArray(ident, size);
        }

        private object[][] createArray(char ident, int size) {
            object[][] newArray = { new object[size + 1], new object[size + 1], new string[size + 1], new string[size+1] };
            newArray[1][0] = ident;
            for (int i = 0; i <= size; i++) {
                newArray[(int)Rows.Index][i] = i;
                newArray[(int)Rows.Positives][i] = "";
                newArray[(int)Rows.Negatives][i] = "";
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

        public object retrieveValue(string item) {
            if (item[0] != this.identifier) {
                throw new ArgumentException("Identifier does not match Index provided: " + item);
            }
            int column = Convert.ToInt32(item.Substring(1));
            if (this.size < column) {
                throw new IndexOutOfRangeException("Index (" + item + ") not within Line " + this.identifier);
            }
            return this.innerArray[(int)Line.Rows.Values][column];
        }

        public void addRelation(string p1, string p2, Line.Rows row = Line.Rows.Positives) {
            if (p1[0] != identifier) {
                throw new ArgumentException("Identifier does not match target location: " + p1);
            }
            int column = Convert.ToInt32(p1.Substring(1));
            if (this.size < column) {
                throw new ArgumentException("Target is out of bounds: " + p1);
            }
            this.innerArray[(int)row][column] += p2;
        }

        public string checkForMatch(string p, Rows row = Rows.Positives) {
            if (string.IsNullOrEmpty(p)) return null;
            //return category item that relates to item passed.
            int unknowns = 0;
            char category = p[0];
            string result = null;
            for (int i = 1; i <= size && unknowns < 2; i++) {
                if (this.innerArray[(int)row][i].ToString().Contains(p)) {
                    return this.identifier + (i).ToString();
                }
                if (!this.innerArray[(int)row][i].ToString().Contains(category)) {
                    unknowns++;
                    result = this.identifier + (i).ToString();
                }
            }
            return unknowns < 2 ? result : null;
        }

        internal string findItem(object targetValue) {
            for (int i = 1; i <= this.size; i++) {
                if (innerArray[(int)Rows.Values][i] == targetValue) {
                    return this.identifier + i.ToString();
                }
            }
            return null;
        }

        internal string findTarget(object knownValue, string comparative) {
            object targetValue = calculator.calculateValue(knownValue, comparative);
            return findItem(targetValue);
        }
    }
}
    