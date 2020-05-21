using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication
{
    public class CurrencyConverter
    {
        public decimal ConvertToGbp(decimal value, decimal exchangeRate, int decimalPlaces)
        {
            if (exchangeRate <= 0)
            {
                throw new ArgumentException(
                "Exchange rate must be greater than zero",
                nameof(exchangeRate));
            }

            var valueInGbp = value / exchangeRate;
            return decimal.Round(valueInGbp, decimalPlaces);
        }
    }
}
