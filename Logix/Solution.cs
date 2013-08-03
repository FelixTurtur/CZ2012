using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix
{
    public delegate void SolutionUpdateHandler (Solution sender, SolutionUpdateArgs e);
    
    public class Solution
    {
        int[,] matrix;
        char LEFTCHAR = 'A';
        public event SolutionUpdateHandler Updater;

        public Solution(int breadth, int depth) {
            matrix = new int[depth, breadth];
        }
        internal bool isComplete() {
            for (int x = 0; x < matrix.GetLength(0); x++) {
                if (!rowComplete(x)) {
                    return false;
                }
            }
            return true;
        }
        private bool rowComplete(int x) {
            for (int y = 0; y < matrix.GetLength(1); y++) {
                if (matrix[x, y] == 0) {
                    return false;
                }
            }
            return true;
        }
        private bool rowEmpty(int x) {
            for (int y = 0; y < matrix.GetLength(1); y++) {
                if (matrix[x, y] != 0) {
                    return false;
                }
            }
            return true;
        }

        private void addItem(int x, string item) {
            int y = item[0] - LEFTCHAR;
            int val = Convert.ToInt32(item.Substring(1));
            matrix[x, y] = val;
        }

        private void onUpdate(SolutionUpdateArgs a) {
            if (Updater != null) {
                Updater(this, a);
            }
        }
        internal List<Relation> considerRelationInSolution(Relation r) {
            if (!r.isDirect() || !r.isPositive()) { return new List<Relation> { r }; }
            string item1 = r.getLeftItem();
            string item2 = r.getRightItem();
            List<int> possibles = new List<int>();
            for (int x = 0; x < matrix.GetLength(0) && possibles.Count < 2; x++) {
                if (rowEmpty(x) && possibles.Count == 0) {
                    addItem(x, item1);
                    addItem(x, item2);
                    return null;
                }
                if (alreadyKnown(r, x)) {
                    return null;
                }
                var newRules = oneItemFound(x, item1, item2);
                if (newRules != null) {
                    return newRules;
                }
                if (otherCategoryMention(x, item1, item2)) {
                    continue;
                }
                else
                    possibles.Add(x);
            }
            if (possibles.Count > 1) {
                return new List<Relation> {r};
            }
            addItem(possibles[0], item1);
            addItem(possibles[0], item2);
            return null;
        }

        private bool alreadyKnown(Relation r, int x) {
            bool leftMatch = matrix[x, r.getLeftItem()[0]-LEFTCHAR] == Convert.ToInt32(r.getLeftItem()[1].ToString());
            bool rightMatch = matrix[x, r.getRightItem()[0] - LEFTCHAR] == Convert.ToInt32(r.getRightItem()[1].ToString());
            return leftMatch && rightMatch;
        }

        private List<Relation> oneItemFound(int x, string item1, string item2) {
            if (matrix[x, item1[0] - LEFTCHAR].ToString() == item1[1].ToString()) {
                addItem(x, item2);
                return createFoundRules(x, item2, item1);
            }
            else if (matrix[x, item2[0] - LEFTCHAR].ToString() == item2[1].ToString()) {
                addItem(x, item1);
                return createFoundRules(x, item1, item2);
            }
            return null;
        }

        private List<Relation> createFoundRules(int x, string newItem, string oldItem) {
            List<Relation> results = new List<Relation>();
            RelationFactory factory = RelationFactory.getInstance();
            for (int y = 0; y < matrix.GetLength(1); y++) {
                if (y == newItem[0] - LEFTCHAR || y == oldItem[0] - LEFTCHAR) {
                    continue;
                }
                else if (matrix[x, y] != 0) {
                    results.Add(factory.createRelation(newItem, Convert.ToChar(LEFTCHAR+y) + matrix[x,y].ToString() , true));
                }
            }
            return results;
        }

        private bool otherCategoryMention(int x, string item1, string item2) {
            return (matrix[x, item1[0] - LEFTCHAR] != 0) || (matrix[x, item2[0] - LEFTCHAR] != 0);
        }

        internal List<Relation> checkAllButOnes() {
            //Check for deductible categories
            List<Relation> results = new List<Relation>();
            for (int y = 0; y < matrix.GetLength(1); y++) {
                List<int> empties = new List<int>();
                for (int x = 0; x < matrix.GetLength(0); x++) {
                    if (matrix[x, y] == 0) {
                        empties.Add(x);
                    }
                }
                if (empties.Count() == 1) {
                    results.AddRange(completeAllButOne(empties[0], y));
                }
            }
            return results;
        }

        private List<Relation> completeAllButOne(int x, int y) {
            //category of y can be completed
            string newItem = getMissingItem(y);
            addItem(x, newItem);
            List<Relation> relations = new List<Relation>();
            for (int cat = 0; cat < matrix.GetLength(1); cat++) {
                if (cat == y || matrix[x,cat] == 0) {
                    continue;
                }
                string thisItem = Convert.ToChar(LEFTCHAR+cat).ToString() + matrix[x,cat].ToString();
                relations.Add(RelationFactory.getInstance().createRelation(newItem, thisItem, true));
            }
            return relations;
        }

        private string getMissingItem(int cat) {
            //requires an unassigned category item
            string item = Convert.ToChar(LEFTCHAR+cat).ToString();
            int[] options = new int[matrix.GetLength(0)+1];
            options[0] = 1;
            for (int x = 0; x < matrix.GetLength(0); x++) {
                options[matrix[x,cat]] = 1;
            }
            for (int n = 1; n <= matrix.GetLength(0); n++) {
                if (options[n]==0) {
                    return item + n;
                }
            }
            return null;
        }

        internal int[,] getFinalMatrix() {
            return matrix;
        }

    }
}
