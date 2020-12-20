using System;
using System.Collections.Generic;
using Library;

namespace Task_1._1
{
    class Program
    {
        static HashSet<int> _identificators = new HashSet<int>();
        static void Main(string[] args)
        { 
            Account accountEUR = new Account(UniqueIdGenerator.GenerateUniqueId(),"EUR");
            Account accountUSD = new Account(UniqueIdGenerator.GenerateUniqueId(),"USD");
            Account accountUAH = new Account(UniqueIdGenerator.GenerateUniqueId(),"UAH");

            accountEUR.Deposit(10, "EUR");
            accountEUR.Withdraw(3, "UAH");
            
            accountUAH.Deposit(121, "USD");
            try
            {
                accountUSD.Withdraw(5, "USD");
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException ||
                    exception is NotSupportedException)
                {
                    Console.WriteLine(exception.Message);
                }
                else
                    throw;
            }

            try
            {
                Account accountPLN = new Account(3, "PLN");
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"Account with currency {accountEUR.Currency} has {accountEUR.GetBalance(accountEUR.Currency)} balance");
            Console.WriteLine($"Account with currency {accountUSD.Currency} has {accountUSD.GetBalance(accountUSD.Currency)} balance");
            Console.WriteLine($"Account with currency {accountUAH.Currency} has {accountUAH.GetBalance(accountUAH.Currency)} balance");
        }

    }
}