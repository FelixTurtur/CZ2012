using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Representation;

namespace Logix.Calculators
{
    public class NumberCalculator : Calculator
    {
        public override object calculateValue(object knownValue, string comparative) {
            int baseValue = (int)knownValue;
            char op = comparative[0];
            int difference = Convert.ToInt32(comparative.Substring(1));
            switch (op) {
                case '+':
                    return baseValue + difference;
                case '-':
                    return baseValue - difference;
                case '*':
                    return baseValue * difference;
                case '/':
                    return baseValue / difference;
                default:
                    throw new ArgumentException("No known numeric operator equivalent to entered char: " + op);
            }
        }

        public override List<int> getImpossibles(int index, string comparator, int size) {
            List<int> results = new List<int>();
            if (Representation.Relations.checkDirection(comparator) == Representation.Relations.Directions.Lower) {
                for (int i = index+1; i <= size; i++) {
                    results.Add(i);
                }
            }
            else {
                for (int i = index-1; i > 0; i--) {
                    results.Add(i);
                }
            }
            return results;
        }

        public override bool checkPredicate(object item, string comparator, string bound) {
            try {
                if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                    return Convert.ToInt32(item) < Convert.ToInt32(item);
                }
                else {
                    return Convert.ToInt32(item) > Convert.ToInt32(item);
                }
            }
            catch (Exception e) {
                throw new LogicException(this.GetType().Name + " unable to check predicate: " + item.ToString() + comparator + bound, e);
            }
        }
    }
}
