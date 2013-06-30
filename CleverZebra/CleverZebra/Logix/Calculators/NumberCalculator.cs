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
    }
}
