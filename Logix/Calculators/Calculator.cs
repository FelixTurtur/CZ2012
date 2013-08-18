using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix.Calculators
{
    /*
     * Calculator subclasses are used to identify one related item from another that is a known distance apart. 
     * For categories that can be compared, that category should keep the correct sort of calculator so that it can match rules.
     */
    public class Calculator
    {
        public virtual object calculateValue(object knownValue, string comparative) {
            return null;
        }

        public virtual bool checkPredicate(object item, string comparator, string bound) {
            return false;
        }
 
        public virtual List<int> getImpossibles(int index, string comparator, int size) {
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

   }
}
