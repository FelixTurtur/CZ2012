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

        public override object calculateValue(object knownValue, string comparative) {
            char op = comparative[0];
            int difference = Convert.ToInt32(comparative.Substring(1));
            int knownDayIndex = findDay(knownValue);
            switch (op) {
                case '+' :
                    return DAYS[(knownDayIndex + difference) % DAYS.Count];
                case '-' :
                    if ((knownDayIndex - difference) >= 0) {
                        return DAYS[(knownDayIndex - difference)];
                    }
                    return DAYS[(knownDayIndex - difference) + DAYS.Count];
                default:
                    throw new ArgumentException("No known date operator equivalent to entered char: " + op);
            }
        }

        private int findDay(object knownValue) {
            for (int i = 0; i < DAYS.Count; i++) {
                if (DAYS[i] == knownValue) {
                    return i;
                }
            }
            throw new ArgumentException("Unrecognised day: " + knownValue);
        }

        public override List<int> getImpossibles(int index, string comparator, int size) {
            List<int> results = new List<int>();
            if (Representation.Relations.checkDirection(comparator) == Representation.Relations.Directions.Lower) {
                for (int i = index + 1; i <= size; i++) {
                    results.Add(i);
                }
            }
            else {
                for (int i = index - 1; i > 0; i--) {
                    results.Add(i);
                }
            }
            return results;
        }

        public override bool checkPredicate(object item, string comparator, string bound) {
            try {
                if (Representation.Relations.checkDirection(comparator) == Relations.Directions.Lower) {
                    return findDay(item) < findDay(bound);
                }
                else {
                    return findDay(item) > findDay(bound);
                }
            }
            catch (Exception e) {
                throw new LogicException(this.GetType().Name + " unable to check predicate: " + item.ToString() + comparator + bound, e);
            }
        }
    }
}
