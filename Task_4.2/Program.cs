using System;
using Library;
using Library.Exceptions;

namespace Task_4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------test_1--------------------");
            CreditCard creditCard = new CreditCard();
            
            try
            {
                creditCard.StartDeposit(1, 5000, "UAH"); // exception because of exceeding of Internet limit 
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount");
            }

            Console.WriteLine("\n--------------------test_2--------------------");
            Privet48 privet48 = new Privet48();
            privet48.StartDeposit(2, 8000, "UAH");
            
            try
            {
                privet48.StartWithdrawal(2, 4000, "UAH"); // exception because sum of transactions is more than 10000 UAH
                // transaction = deposit + withdrawal 
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount");
            }
            
            Console.WriteLine("\n--------------------test_3--------------------");
            Stereobank stereobank = new Stereobank();
            
            try
            {
                stereobank.StartDeposit(3, 106, "USD"); // exception because of exceeding of Internet limit 
                
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount");
            }
            
            Console.WriteLine("\n--------------------test_4--------------------");
            try
            {
                stereobank.StartDeposit(3, 200, "EUR"); // exception because sum of transactions is more than 7000 UAH
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount");
            }
            
            Console.WriteLine("\n--------------------test_5--------------------");
            GiftVoucher giftVoucher = new GiftVoucher();
            giftVoucher.StartDeposit(4, 100, "USD"); // voucher number: 1234567890

            try
            {
                giftVoucher.StartDeposit(4, 100, "USD"); // voucher number: 1234567890
                // exception because voucher was already used
            }
            catch (InsufficientFundsException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
            }
            
        }
    }
}