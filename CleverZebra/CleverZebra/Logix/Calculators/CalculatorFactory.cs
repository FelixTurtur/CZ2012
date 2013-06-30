using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Logix.Calculators
{
    public class CalculatorFactory
    {
        static CalculatorFactory instance = null;

        private CalculatorFactory() { }

        public static CalculatorFactory getInstance() {
            if (instance == null) {
                instance = new CalculatorFactory();
            }
            return instance;
        }

        public Calculator createCalculator(string keyword) {
            switch (keyword) {
                case "Numeric":
                    return new NumberCalculator();
                case "Date":
                    return new DateCalculator();
                default:
                    throw new ArgumentException("No Calculator available for keyword " + keyword);
            }
        }

    }
}
