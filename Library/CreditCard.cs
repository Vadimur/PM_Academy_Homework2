using System;
using Library.Exceptions;

namespace Library
{
    public class CreditCard : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public CreditCard()
        {
            Name = "CreditCard";
        }
        public void StartDeposit(int playerId, decimal amount, string currency)
        {
            if (amount <= 0 )
                throw new InvalidOperationException("Invalid deposit amount");
            
            if (!IsTransactionValid(amount, currency))
                throw new LimitExceededException();
            string cardNumber = ReadCardNumber();
            string expiryDate = ReadExpiryDate();
            string cvv = ReadCVV();
            Console.WriteLine($"You’ve withdraw {amount} {currency} from your {cardNumber} successfully");
        }

        public void StartWithdrawal(int playerId, decimal amount, string currency)
        {
            if (amount <= 0 )
                throw new InvalidOperationException("Invalid withdrawal amount");
            
            if (!IsTransactionValid(amount, currency))
                throw new LimitExceededException();
            string cardNumber = ReadCardNumber();
            Console.WriteLine($"You’ve deposit {amount} {currency} to your {cardNumber} successfully");
        }

        private string ReadCardNumber()
        {
            string cardNumber;
            do
            {
                Console.WriteLine("Enter card number, please");
                cardNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(cardNumber)|| cardNumber.Length != 16) continue;
                if (cardNumber.StartsWith('4') || cardNumber.StartsWith('5'))
                {
                    break;
                }

            } while (true);

            return cardNumber;
        }

        private string ReadExpiryDate()
        {
            string expiryDate;
            do
            {
                Console.WriteLine("Enter expiry date, please");
                expiryDate = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(expiryDate)|| expiryDate.Length != 5) continue;
                if (!expiryDate[2].Equals('/')) continue;
                
                string rawMonth = expiryDate.Substring(0, 2);
                string rawYear = expiryDate.Substring(3, 2);
                
                if (!int.TryParse(rawMonth, out int month) || !int.TryParse(rawYear, out int year)) continue;
                
                if (month >= 1 && month <= 12 && year >= 20) // year >= 20 because in other way this card is expired
                {
                    break;
                }

            } while (true);

            return expiryDate;
        }
        
        private string ReadCVV()
        {
            string cvv;
            do
            {
                Console.WriteLine("Enter CVV, please");
                cvv = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(cvv) || cvv.Length != 3) continue;
                if (int.TryParse(cvv, out int x))
                {
                    break;
                }
            } while (true);

            return cvv;
        }

        private bool IsTransactionValid(decimal amount, string currency)
        {
            decimal amountUAH = CurrencyExchanger.ConvertCurrency(currency, "UAH", amount);
            return amountUAH <= 3000;
        }
    }
}