using CleverZebra.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra.Logix.Calculators
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
            if (Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                for (int i = index; i <= size; i++) {
                    results.Add(i);
                }
            }
            else {
                for (int i = index; i > 0; i--) {
                    results.Add(i);
                }
            }
            return results;
        }
    }
}
