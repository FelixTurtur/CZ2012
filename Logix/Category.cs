using System;
using System.Linq;
using Logix.Calculators;
using Representation;
using System.Collections.Generic;

namespace Logix
{
   public delegate void MatchEventHandler (Category sender, MatchEventArgs e);
 
   /* Category class 
     * Creates a matrix that will store information on one category. The matrix is always four rows deep with one labelling column
     * and then one column per value within the category. The top row holds the index, the second the value (if necessary) and the 
     * third/fourth any known positive/negative relational data so that logical deductions can be drawn. 
     * Rules keeps a copy of keywords relevant to the category.
     * The Identifier is the [A|B|C|D|E...] puzzle-scope character for this category.
     * */
    public class Category
    {
       public event MatchEventHandler Matched; 
       
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

        private void OnMatch(MatchEventArgs e) {
           if (Matched != null) {
              Matched(this, e);
           }
        }
 
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

        internal void setKeyword(string key) {
            this.keyword = key;
            calculator = CalculatorFactory.getInstance().createCalculator(key);
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
            int column = Convert.ToInt32(p1[1].ToString());
            if (this.size < column) {
                throw new ArgumentException("Target is out of bounds: " + p1);
            }
            if (!this.innerArray[(int)row][column].ToString().Contains(p2)) {
                this.innerArray[(int)row][column] += p2;
                if (row == Rows.Positives) {
                   OnMatch(new MatchEventArgs(p1, p2));
                    createNegativeLinkForOthers(p2, column);
                }
            }
        }

        public string checkForMatch(string p, Rows row = Rows.Positives) {
            if (string.IsNullOrEmpty(p)) return null;
            //return category item that relates to item passed.
            int finds = 0;
            char category = p[0];
            string result = null;
            if (row == Rows.Positives) {
                for (int i = 1; i <= size; i++) {
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
                if (this.innerArray[(int)Rows.Positives][i].ToString().Contains(p)) return null;
                if (this.innerArray[(int)row][i].ToString().Contains(p)) {
                    finds++;
                }
                else {
                    result = this.identifier + (i).ToString();
                }
            }
            return finds == size - 1 ? result : null;
        }

        internal string findTarget(object knownValue, string comparative) {
            object targetValue = calculator.calculateValue(knownValue, comparative);
            return findItem(targetValue);
        }

        internal string findItem(object targetValue) {
            for (int i = 1; i <= this.size; i++) {
                if (innerArray[(int)Rows.Values][i].Equals(targetValue)) {
                    return this.identifier + i.ToString();
                }
            }
            return null;
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
            string[] negatives = getAllListedItems(Rows.Negatives);
            if (negatives != null) {
                foreach (string item in negatives.Distinct<string>()) {
                    //check if it is listed against all but one
                    string item2 = checkForMatch(item, Rows.Negatives);
                    if (!string.IsNullOrEmpty(item2)) {
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

        public List<Relation> considerRelationToCategory(Relation r, bool alreadySeen) {
            try {
                if (!r.getRule().Contains(identifier)) {
                    //cannot use this relation
                    return new List<Relation> { r };
                }
                if (r.isConditional()) {
                    if (((ConditionalRelation)r).getRule().Contains(identifier)) {
                        bool? conditionMet = checkDeterminability(r);
                        if (conditionMet.HasValue) {
                            if (conditionMet.Value) {
                                return new List<Relation> { RelationFactory.getInstance().createRelation(((ConditionalRelation)r).getIfTrueStatement()) };
                            }
                            return new List<Relation> { RelationFactory.getInstance().createRelation(((ConditionalRelation)r).getIfFalseStatement()) };
                        }
                    }
                    return new List<Relation> { r };
                }
                if (r.isDirect()) {
                    //direct relation
                    if (!alreadySeen) {
                        Category.Rows row = r.isPositive() ? Category.Rows.Positives : Category.Rows.Negatives;
                        addRelation(r.getBaseItem(identifier), r.getRelatedItem(identifier), row);
                    }
                    return null;
                }
                if (r.isRelative()) {
                    string[] items = { r.getBaseItem(identifier, Relations.Sides.Left), r.getBaseItem(identifier, Relations.Sides.Right) };
                    if (items[0] == items[1]) {
                        //Single-item Relative
                        if (items[0] != identifier.ToString()) {
                            //cannot discover anything within this category
                            return new List<Relation> { r };
                        }
                        string relatedItem = r.getRelatedItem(identifier);
                        return createImpossiblesForItem(relatedItem, Relations.getComparator(r.getRule()), Relations.getComparativeAmount(r.getRule(), false));
                    }
                    string leftMatch = checkForMatch(items[0]);
                    string rightMatch = checkForMatch(items[1]);
                    List<Relation> results = new List<Relation>();
                    if (!string.IsNullOrEmpty(leftMatch) && !string.IsNullOrEmpty(rightMatch)) {
                        //both sides already matched 
                        return null;
                    }
                    else if (!string.IsNullOrEmpty(leftMatch) || !string.IsNullOrEmpty(rightMatch)) {
                        //if either of the two items has a value, then something can be learnt for the other, if not a complete match
                        if (Representation.Relations.isQuantified(r.getRule())) {
                            string unknownItem = leftMatch == null ? items[0] : items[1];
                            object knownValue = retrieveValue(leftMatch ?? rightMatch);
                            bool inverse = leftMatch == null ? true : false;
                            string targetItem = findTarget(knownValue, Relations.getComparativeAmount(r.getRule(), inverse));
                            addRelation(targetItem, unknownItem, r.isPositive() ? Category.Rows.Positives : Category.Rows.Negatives);
                            results.Add(RelationFactory.getInstance().createRelation(targetItem, unknownItem, r.isPositive()));
                        }
                        else {
                            results = considerComparative(leftMatch ?? items[0], Relations.getComparator(r.getRule()), rightMatch ?? items[1]);
                        }
                        return results;
                    }
                    else if (items[0] != null && items[1] != null) {
                        //create negative relations from comparatives
                        results = (createNegativeRelationsToBounds(items[0], items[1], r.getComparator(), Relations.getComparativeAmount(r.getRule(), false), alreadySeen));
                        if (results == null) results = new List<Relation> { r };
                        else results.Add(r);
                    }
                    return results;
                }
                throw new ArgumentException("Relation type not recognised: " + r.GetType().ToString());
            }
            catch (Exception e) {
                throw e;
            }
        }

        private List<Relation> createImpossiblesForItem(string relatedItem, string comparator, string bound) {
            try {
                List<Relation> results = new List<Relation>();
                for (int i = 0; i < size; i++) {
                    if (calculator.checkPredicate(innerArray[(int)Rows.Values][i], comparator, bound)) {
                        continue;
                    }
                    else {
                        results.Add(RelationFactory.getInstance().createRelation(relatedItem, identifier.ToString() + i, false));
                    }
                }
                return results;
            }
            catch (LogicException l) {
                throw l;
            }
        }

        private bool? checkDeterminability(Relation r) {
            Relation testRelation = ((ConditionalRelation)r).conditional;
            if (testRelation.isRelative()) {
                //wuss out until we need this
                return null;
            }
            else {
                string baseItem = testRelation.getBaseItem(this.identifier);
                string relatedItem = testRelation.getRelatedItem(this.identifier);
                if (innerArray[(int)Rows.Positives][Convert.ToInt32(baseItem[1].ToString())].ToString().Contains(relatedItem)) {
                    if (testRelation.isPositive()) {
                        return true; //Positive rule, positive match
                    }
                    return false; //Negative rule, positive match
                }
                else if (innerArray[(int)Rows.Negatives][Convert.ToInt32(baseItem[1].ToString())].ToString().Contains(relatedItem)) {
                    if (testRelation.isPositive()) {
                        return false; //Positive rule, negative match
                    }
                    return true; //Negative rule, negative match
                }
                return null;
            }
        }

        internal List<Relation> considerComparative(string p1, string comparator, string p2) {
            string matchedIndex = "";
            string itemToRelate = "";
            List<Relation> results = new List<Relation>();
            matchedIndex = p1[0] == identifier ? p1 : p2;
            itemToRelate = p1[0] == identifier ? p2 : p1;
            comparator = p1[0] == identifier ? comparator : Relations.getInverse(comparator);
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

        private void createNegativeLinkForOthers(string p2, int y) {
            for (int i = 1; i <= size; i++) {
                if (i == y) {
                    continue;
                }
                addRelation(identifier.ToString() + i, p2, Rows.Negatives);
            }
        }

        private List<Relation> createNegativeRelationsToBounds(string leftItem, string rightItem, string comparator, string difference, bool alreadySeen) {
            RelationFactory relationBuilder = RelationFactory.getInstance();
            List<Relation> relations = new List<Relation>();

            if (difference == null) { //not a Quantified Relation
                string inverseComparator = Relations.getInverse(comparator);
                for (int i = 1; i <= size; i++) {
                    if (hasNoPossibleOpposite(leftItem, i, comparator, rightItem, alreadySeen)) {
                        relations.Add(relationBuilder.createRelation(leftItem, identifier.ToString() + i, false));
                    }
                    if (hasNoPossibleOpposite(rightItem, i, inverseComparator, leftItem, alreadySeen)) {
                        relations.Add(relationBuilder.createRelation(rightItem, identifier.ToString() + i, false));
                    }
                }
            }
            else {
                string calculatedItem = "";
                string inverseDifference = Relations.getInverse(difference[0].ToString()) + difference.Substring(1);
                for (int i = 1; i <= size; i++) {
                    calculatedItem = findTarget(innerArray[(int)Rows.Values][i], difference);
                    if (calculatedItem == null) {
                        relations.Add(relationBuilder.createRelation(leftItem, identifier + i.ToString(), false));
                    }
                    calculatedItem = findTarget(innerArray[(int)Rows.Values][i], inverseDifference);
                    if (calculatedItem == null) {
                        relations.Add(relationBuilder.createRelation(rightItem, identifier + i.ToString(), false));
                    }
                }
            }
            relations.AddRange(checkDeductibles());
            return relations;
        }

        private bool hasNoPossibleOpposite(string item1, int index, string comparator, string item2, bool alreadySeen) {
            if (index == 1 && Relations.checkDirection(comparator) == Relations.Directions.Higher) {
                return !alreadySeen;
            }
            if (index == size && Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                return !alreadySeen;
            }
            if (innerArray[(int)Rows.Negatives][index].ToString().Contains(item1)) {
                return false;
            }
            List<int> indexesToCheck = this.calculator.getImpossibles(index, comparator, this.size);
            foreach (int i in indexesToCheck) {
                if (!innerArray[(int)Rows.Negatives][i].ToString().Contains(item2)) {
                    return false;
                }
            }
            return true;
        }
    }
}
    