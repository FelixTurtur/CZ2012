using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra.Logix.Calculators
{
    /*
     * Calculator subclasses are used to identify one related item from another that is a known distance apart. 
     * For categories that can be compared, that category should keep the correct sort of calculator so that it can match rules.
     */
    public abstract class Calculator
    {
        public abstract object calculateValue(object knownValue, string comparative);
    }
}
