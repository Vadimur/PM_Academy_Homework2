using System;
using System.Collections.Generic;

namespace Library
{
     public class Account
     {
         public readonly int Id;
         public readonly string Currency;
         private decimal _amount = 0;

         public Account(int id, string currency)
         {
             currency = currency.ToUpper();
            if (!currency.Equals("USD") && !currency.Equals("EUR") && !currency.Equals("UAH"))
                throw new NotSupportedException("Not supported currency");
            Id = id;
            Currency = currency;

         }

        public void Deposit(decimal amount, string currency)
        {
            if (amount <= 0 )
                throw new InvalidOperationException("Invalid deposit amount");

            if (Currency.Equals(currency.ToUpper()))
                _amount += amount;
            else
                _amount += CurrencyExchanger.ConvertCurrency(currency, Currency, amount);
        }

        public void Withdraw(decimal amount, string currency)
        {
            decimal amountToWithdraw = 0;
            if (Currency.Equals(currency.ToUpper()))
                amountToWithdraw =  amount;
            else
                amountToWithdraw = CurrencyExchanger.ConvertCurrency(currency, Currency, amount);
            
            if (_amount < amountToWithdraw)
                throw new InvalidOperationException("Invalid withdraw amount");

            _amount -= amountToWithdraw;
            
        }

        public decimal GetBalance(string currency)
        {
            if (Currency.Equals(currency.ToUpper()))
                return _amount;
            else
                return CurrencyExchanger.ConvertCurrency(Currency, currency, _amount);
        }

    }
}