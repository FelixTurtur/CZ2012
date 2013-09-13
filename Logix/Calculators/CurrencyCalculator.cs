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
                    return getCurrencyUnit(knownValue) + (knownAmount + difference).ToString() + getPostAmountTerm(knownValue);
                case '-':
                    return getCurrencyUnit(knownValue) + (knownAmount - difference).ToString() + getPostAmountTerm(knownValue);
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
            string amount = "";
            string postamount = "";
            bool prenumber = true;
            int j = 0;
            for (int n = 0; n < knownValue.ToString().Length; n++) {
                if (Int32.TryParse(knownValue.ToString()[n].ToString(), out j)) {
                    amount += j;
                    if (prenumber) prenumber = false;
                }
                else if (prenumber) {
                    currency += knownValue.ToString()[n];
                }
                else if (knownValue.ToString()[n] == '.') {
                    amount += '.';
                }
                else {
                    postamount += knownValue.ToString()[n];
                }
            }
            return Convert.ToDouble(amount);
        }

        private string getPostAmountTerm(object knownValue) {
            string currency = "";
            string amount = "";
            string postamount = "";
            bool prenumber = true;
            int j = 0;
            for (int n = 0; n < knownValue.ToString().Length; n++) {
                if (Int32.TryParse(knownValue.ToString()[n].ToString(), out j)) {
                    amount += j;
                    if (prenumber) prenumber = false;
                }
                else if (prenumber) {
                    currency += knownValue.ToString()[n];
                }
                else if (knownValue.ToString()[n] == '.') {
                    amount += '.';
                }
                else if (knownValue.ToString()[n] == ',') {
                    amount += ',';
                }
                else {
                    postamount += knownValue.ToString()[n];
                }
            }
            return postamount;

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
