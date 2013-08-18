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
            int baseValue = Convert.ToInt32(knownValue);
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

        public override bool checkPredicate(object item, string comparator, string bound) {
            try {
                if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                    return Convert.ToInt32(item) < Convert.ToInt32(bound);
                }
                else {
                    return Convert.ToInt32(item) > Convert.ToInt32(bound);
                }
            }
            catch (Exception e) {
                throw new LogicException(this.GetType().Name + " unable to check predicate: " + item.ToString() + comparator + bound, e);
            }
        }
    }
}
