using Representation;
using System;

namespace Logix.Calculators
{
    public class CurrencyCalculator : Calculator
    {
        public override object calculateValue(object knownValue, string comparative) {
            double knownAmount = getCurrencyAmount(knownValue);
            char op = comparative[0];
            double difference = getCurrencyAmount(comparative);
            switch (op) {
                case '+':
                    return getCurrencyUnit(knownValue) + (knownAmount + difference).ToString();
                case '-':
                    return getCurrencyUnit(knownValue) + (knownAmount - difference).ToString();
                default:
                    throw new ArgumentException("No known currency operator equivalent to provided char: " + op);
            }
        }

        private string getCurrencyUnit(object knownValue) {
            string currency = "";
            int i = 0;
            int j = 0;
            while (!Int32.TryParse(knownValue.ToString()[i].ToString(), out j)) {
                currency += knownValue.ToString()[i];
                i++;
            }
            return currency;
        }

        private static double getCurrencyAmount(object knownValue) {
            string currency = "";
            int i = 0;
            int j = 0;
            while (!Int32.TryParse(knownValue.ToString()[i].ToString(), out j)) {
                currency += knownValue.ToString()[i];
                i++;
            }
            return Convert.ToDouble(knownValue.ToString().Substring(currency.Length).Replace(",", ""));
        }

        public override bool checkPredicate(object item, string comparator, string bound) {
            try {
                if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                    return getCurrencyAmount(item) < getCurrencyAmount(bound);
                }
                else {
                    return getCurrencyAmount(item) > getCurrencyAmount(bound);
                }
            }
            catch (Exception e) {
                throw new LogicException(this.GetType().Name + " unable to check predicate: " + item.ToString() + comparator + bound, e);
            }
        }
    }
}
