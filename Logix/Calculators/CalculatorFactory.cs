using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Calculators
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
            switch (keyword.ToLower()) {
                case "numeric":
                case "years":
                    return new NumberCalculator();
                case "months":
                case "days":
                case "dates":
                    return new DateCalculator(keyword);
                case "time":
                    return new TimeCalculator();
                case "ordinals":
                    return new OrdinalCalculator();
                case "currency":
                    return new CurrencyCalculator();
                case "letters":
                case "words":
                    return new LengthCalculator(keyword);
                default:
                    throw new ArgumentException("No Calculator available for keyword " + keyword);
            }
        }

    }
}
