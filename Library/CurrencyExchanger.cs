using System;

namespace Library
{
    public static class CurrencyExchanger
    {
        private const decimal USDtoUAH = 28.36m;
        private const decimal EURtoUAH = 33.63m;
        private const decimal EURtoUSD = 1.19m;

        public static decimal ConvertCurrency(string fromCurrency, string toCurrency, decimal fromAmount)
        {
            toCurrency = toCurrency.ToUpper();
            fromCurrency = fromCurrency.ToUpper();
            if (!fromCurrency.Equals("USD") && !fromCurrency.Equals("EUR") && !fromCurrency.Equals("UAH"))
                throw new NotSupportedException($"Not supported currency: {fromCurrency}");
            
            if (!toCurrency.Equals("USD") && !toCurrency.Equals("EUR") && !toCurrency.Equals("UAH"))
                throw new NotSupportedException($"Not supported currency: {toCurrency}");

            decimal? result = fromCurrency switch
            {
                "USD" => toCurrency switch
                {
                    "EUR" => fromAmount / EURtoUSD,
                    "UAH" => fromAmount * USDtoUAH,
                    "USD" => fromAmount,
                    _ => null
                },
                "EUR" => toCurrency switch
                {
                    "USD" => fromAmount * EURtoUSD,
                    "UAH" => fromAmount * EURtoUAH,
                    "EUR" => fromAmount,
                    _ => null
                },
                "UAH" => toCurrency switch
                {
                    "EUR" => fromAmount / EURtoUAH,
                    "USD" => fromAmount / USDtoUAH,
                    "UAH" => fromAmount,
                    _ => null
                },
                _ => null
            };

            if (result.HasValue == false)
                throw new NotSupportedException($"Not supported currency: {toCurrency}");
            
            return result.Value;
        }
    }
}