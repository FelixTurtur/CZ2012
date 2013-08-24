using Representation;
using System;
using System.Linq;

namespace Logix.Calculators
{
    public class LengthCalculator : Calculator
    {
        private string keyword;

        public LengthCalculator(string keyword) {
            this.keyword = keyword;
        }
        public override object calculateValue(object knownValue, string comparative) {
            int baseValue = 0;
            if (keyword == "letters") {
                baseValue = knownValue.ToString().Length;
            }
            else {
                //keyword == words
                baseValue = knownValue.ToString().Split(new char[] { ' ' }).Count();
            }
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
                    throw new ArgumentException("No known length operator equivalent to provided char: " + op);
            }
        }

        public override bool checkPredicate(object item, string comparator, string bound) {
            if (keyword == "letters") {
                if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                    return item.ToString().Length < bound.Length;
                }
                else {
                    return item.ToString().Length > bound.Length;
                }
            }
            else {
                if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                    return item.ToString().Split(new char[] { ' ' }).Count() < bound.Split(new char[] { ' ' }).Count();
                }
                else {
                    return item.ToString().Split(new char[] { ' ' }).Count() > bound.Split(new char[] { ' ' }).Count();
                }
            }
        }
    }
}
