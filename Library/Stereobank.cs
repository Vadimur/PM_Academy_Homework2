using System.Collections.Generic;

namespace Library
{
    public class Stereobank : Bank
    {
        public Stereobank()
        {
            Name = "Stereobank";
            AvailableCards = new string[]{"Black", "White", "Iron"};
            Limit = 7000;
        }
        
        protected override bool IsTransactionAmountValid(decimal amount, string currency)
        {
            decimal amountUAH = CurrencyExchanger.ConvertCurrency(currency, "UAH", amount);
            return amountUAH <= 3000;
        }

    }
}