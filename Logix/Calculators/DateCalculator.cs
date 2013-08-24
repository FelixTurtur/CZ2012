using Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix.Calculators
{
    class DateCalculator : Calculator
    {
        private static List<string> DAYS = new List<string> {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
        private static List<string> MONTHS = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private static List<string> DATES = new List<string> {"1st", "2nd","3rd","4th","5th","6th","7th","8th","9th","10th","11th","12th","13th","14th","15th","16th","17th","18th","19th","20th","21st","22nd","23rd","24th","25th","26th","27th","28th","29th","30th" };
        private List<string> LIST;

        public DateCalculator(string keyword) {
            switch (keyword) {
                case "days":
                    LIST = DAYS;
                    break;
                case "months":
                    LIST = MONTHS;
                    break;
                default:
                    LIST = DATES;
                    break;
            }
        }

        public override object calculateValue(object knownValue, string comparative) {
            char op = comparative[0];
            int difference = Convert.ToInt32(comparative.Substring(1));
            int knownIndex = find(knownValue);
            switch (op) {
                case '+' :
                    return LIST[(knownIndex + difference) % LIST.Count];
                case '-' :
                    if ((knownIndex - difference) >= 0) {
                        return LIST[(knownIndex - difference)];
                    }
                    return LIST[(knownIndex - difference) + LIST.Count];
                default:
                    throw new ArgumentException("No known date operator equivalent to entered char: " + op);
            }
        }

        private int find(object knownValue) {
            for (int i = 0; i < LIST.Count; i++) {
                if (LIST[i] == knownValue.ToString()) {
                    return i;
                }
            }
            throw new ArgumentException("Unrecognised day: " + knownValue);
        }

        public override bool checkPredicate(object item, string comparator, string bound) {
            try {
                if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                    return find(item) < find(bound);
                }
                else {
                    return find(item) > find(bound);
                }
            }
            catch (Exception e) {
                throw new LogicException(this.GetType().Name + " unable to check predicate: " + item.ToString() + comparator + bound, e);
            }
        }
    }
}
