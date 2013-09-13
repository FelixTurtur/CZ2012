using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix.Calculators
{
    public class TimeCalculator : Calculator
    {
        public override object calculateValue(object knownValue, string comparative) {
            throw new NotImplementedException("Calculator not yet implemented:\n" + this.GetType().Name);
        }

        public override bool checkPredicate(object item, string comparator, string bound) {
            throw new NotImplementedException("Calculator not yet implemented:\n" + this.GetType().Name);
        }

        public override List<int> getImpossibles(int index, string comparator, int size) {
            throw new NotImplementedException("Calculator not yet implemented:\n" + this.GetType().Name);
        }
    }
}
