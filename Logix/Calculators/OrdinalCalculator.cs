using Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix.Calculators
{
    public class OrdinalCalculator : Calculator
    {
        private List<string> ordinalWords = new List<string> { "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "ninth", "tenth", "eleventh", "twelfth", "thirteenth", "fourteenth", "fifteenth", "sixteenth", "seventeenth", "eighteenth", "nineteenth", "twentieth" };
        private List<string> ordinalDigits = new List<string> { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th" };

        public override object calculateValue(object knownValue, string comparative) {
            char op = comparative[0];
            int difference = Convert.ToInt32(comparative.Substring(1));
            int knownOrdinal = findInList(ref ordinalWords, knownValue);
            if (knownOrdinal != -1) {
                switch (op) {
                    case '+':
                        return ordinalWords[knownOrdinal + difference];
                    case '-':
                        return ordinalWords[knownOrdinal - difference];
                    default:
                        throw new ArgumentException("No known Ordinal operator equivalent of " + op);
                }
            }
            else {
                knownOrdinal = findInList(ref ordinalDigits, knownValue);
                if (knownOrdinal == -1) {
                    throw new ArgumentException("Ordinal not found in lists: " + knownValue);
                }
                switch (op) {
                    case '+':
                        return ordinalDigits[knownOrdinal + difference];
                    case '-':
                        return ordinalDigits[knownOrdinal - difference];
                    default:
                        throw new ArgumentException("No known Ordinal operator equivalent of " + op);
                }
            }
        }

        private int findInList(ref List<string> list, object knownValue) {
            for (int i = 0; i < list.Count; i++) {
                if (list[i] == knownValue.ToString()) {
                    return i;
                }
            }
            return -1;
        }

        public override bool checkPredicate(object item, string comparator, string bound) {
            if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                return ordinalWords.Contains(item) ? findInList(ref ordinalWords, item) < findInList(ref ordinalWords, bound)
                    : findInList(ref ordinalDigits, item) < findInList(ref ordinalDigits, bound);
            }
            else {
                return ordinalWords.Contains(item) ? findInList(ref ordinalWords, item) > findInList(ref ordinalWords, bound)
                    : findInList(ref ordinalDigits, item) > findInList(ref ordinalDigits, bound);
            }
        }

    }
}
