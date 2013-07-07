using System;
using System.Linq;
using Logix.Calculators;
using Representation;
using System.Collections.Generic;

namespace Logix
{
    /* Line class 
     * Creates a matrix that will store information on one category. The matrix is always four rows deep with one labelling column
     * and then one column per value within the category. The top row holds the index, the second the value (if necessary) and the 
     * third/fourth any known positive/negative relational data so that logical deductions can be drawn. 
     * Rules keeps a copy of keywords relevant to the category.
     * The Identifier is the [A|B|C|D|E...] puzzle-scope character for this category.
     * */
    public class Category
    {
        public enum Rows
        {
            Index = 0,
            Values = 1,
            Positives = 2,
            Negatives = 3
        };

        public char identifier {get; internal set;}
        public int size {get; internal set;}
        private string keyword;
        private object[][] innerArray;
        private Calculator calculator;

        internal Category() { size = 0; identifier = 'Z'; }

        internal void createInnerArray()
        {
            innerArray = createArray(identifier, size);
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
        
        internal void enterValues(object[] list) {
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
            return this.innerArray[(int)Category.Rows.Values][column];
        }

        public void addRelation(string p1, string p2, Rows row = Rows.Positives) {
            if (p1[0] != identifier) {
                throw new ArgumentException("Identifier does not match target location: " + p1);
            }
            int column = Convert.ToInt32(p1.Substring(1));
            if (this.size < column) {
                throw new ArgumentException("Target is out of bounds: " + p1);
            }
            if (!this.innerArray[(int)row][column].ToString().Contains(p2)) {
                this.innerArray[(int)row][column] += p2;
            }
        }

        public string checkForMatch(string p, Rows row = Rows.Positives) {
            if (string.IsNullOrEmpty(p)) return null;
            //return category item that relates to item passed.
            int finds = 0;
            char category = p[0];
            string result = null;
            if (row == Rows.Positives) {
                for (int i = 1; i <= size && finds < 2; i++) {
                    if (this.innerArray[(int)row][i].ToString().Contains(p)) {
                        return this.identifier + (i).ToString();
                    }
                    if (!this.innerArray[(int)row][i].ToString().Contains(category)) {
                        finds++;
                        result = this.identifier + (i).ToString();
                    }
                }
                return finds < 2 ? result : null;
            }
            for (int i = 1; i <= size; i++) {
                if (this.innerArray[(int)row][i].ToString().Contains(p)) {
                    finds++;
                }
                else {
                    result = this.identifier + (i).ToString();
                }
            }
            return finds == size - 1 ? result : null;
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

        internal List<Relation> considerComparative(string p1, string comparator, string p2) {
            string matchedIndex = "";
            string itemToRelate = "";
            List<Relation> results = new List<Relation>();
            if (p1[0] == identifier) {
                matchedIndex = p1;
                itemToRelate = p2;
            }
            else if (p2[0] == identifier) {
                matchedIndex = p2;
                itemToRelate = p1;
                comparator = Relations.getInverse(comparator);
            }
            List<int> indexesToCheck = this.calculator.getImpossibles(Convert.ToInt32(matchedIndex.Substring(1)), comparator, this.size);
            foreach (int i in indexesToCheck) {
                if (innerArray[(int)Rows.Positives][i].ToString().Contains(itemToRelate[0])) {
                    continue;
                }
                if (innerArray[(int)Rows.Negatives][i].ToString().Contains(itemToRelate[0])) {
                    continue;
                }
                this.addRelation(identifier + i.ToString(), itemToRelate, Rows.Negatives);
                results.Add(RelationFactory.getInstance().createRelation(identifier + i.ToString(), itemToRelate, false));
            }
            return results;
        }

        internal void setKeyword(string key) {
            this.keyword = key;
            calculator = CalculatorFactory.getInstance().createCalculator(key);
        }

        internal static string[] getMatchedItems(Category c, int index) {
            string positivesList = c.innerArray[(int)Rows.Positives][index].ToString();
            return splitItems(positivesList);
        }

        internal static string[] getUnmatchedItems(Category c, int index) {
            string negativesList = c.innerArray[(int)Rows.Negatives][index].ToString();
            return splitItems(negativesList);
        }

        private static string[] splitItems(string list) {
            if (string.IsNullOrEmpty(list)) return null;
            List<string> items = new List<string>();
            string item = list[0].ToString();
            for (int i = 1; i < list.Length; i++) {
                if (Char.IsLetter(list[i])) {
                    items.Add(item);
                    item = list[i].ToString();
                }
                else item += list[i];
            }
            items.Add(item);
            return items.ToArray();
        }

        internal List<Relation> checkDeductibles() {
            List<Relation> relations = new List<Relation>();
            for (int x = 1; x <= size; x++ ) {
                string[] negatives = getAllListedItems(Rows.Negatives);
                if (negatives == null) { continue; }
                foreach (string item in negatives) {
                    string item2 = checkForMatch(item, Rows.Negatives);
                    if(!string.IsNullOrEmpty(item2)) {
                        relations.Add(RelationFactory.getInstance().createRelation(item, item2, true));
                    }
                }
            }
            return relations;
        }

        private string[] getAllListedItems(Rows row) {
            string list = "";
            for (int i = 1; i <= size; i++) {
                list += innerArray[(int)row][i].ToString();
            }
            return splitItems(list);
        }
    }
}
    