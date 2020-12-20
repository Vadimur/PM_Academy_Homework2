using System;
using System.Collections.Generic;
using Library.Exceptions;

namespace Library
{
    public abstract class Bank : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        protected string[] AvailableCards;
        private Dictionary<int, decimal> _proceededTransactions;
        protected decimal Limit;

        protected Bank()
        {
            _proceededTransactions = new Dictionary<int, decimal>();
        }

        public void StartDeposit(int playerId, decimal amount, string currency)
        {
            if (amount <= 0 )
                throw new InvalidOperationException("Invalid deposit amount");
            
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}!");
            Console.WriteLine("Please, enter your login");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password");
            string password = Console.ReadLine();
            Console.WriteLine($"Hello Mr {login}");
            ShowAvailableCards();
            string choice;
            int cardId;
            do
            {
                Console.WriteLine("Pick a card to proceed the transaction");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out cardId) || cardId < 0 || cardId >= AvailableCards.Length);


            if (!IsTransactionSumNotExceeded(playerId, amount, currency))
            {
                throw new LimitExceededException();
            }
            
            if (!IsTransactionAmountValid(amount, currency))
            {
                throw new LimitExceededException();
            }
            
            Console.WriteLine($"You’ve withdraw {amount} {currency} from your {AvailableCards[cardId]} card successfully");
        }

        public void StartWithdrawal(int playerId, decimal amount, string currency)
        {
            if (amount <= 0 )
                throw new InvalidOperationException("Invalid deposit amount");
            
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}!");
            Console.WriteLine("Please, enter your login");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password");
            string password = Console.ReadLine();
            Console.WriteLine($"Hello Mr {login}");
            ShowAvailableCards();
            string choice;
            int cardId;
            do
            {
                Console.WriteLine("Pick a card to proceed the transaction");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out cardId) || cardId < 0 || cardId >= AvailableCards.Length);
            
            if (!IsTransactionAmountValid(amount, currency))
            {
                throw new LimitExceededException();
            }
            
            if (!IsTransactionSumNotExceeded(playerId, amount, currency))
            {
                throw new LimitExceededException();
            }

            Console.WriteLine($"You’ve deposit {amount} {currency} to your {AvailableCards[cardId]} card successfully");
        }

        private bool IsTransactionSumNotExceeded(int playerId, decimal amount, string currency)
        {
            decimal amountUAH = CurrencyExchanger.ConvertCurrency(currency, "UAH", amount);
            
            if (_proceededTransactions.TryGetValue(playerId, out decimal transactionsAmount))
            {
                if (transactionsAmount + amountUAH > Limit)
                    return false;
                    
                _proceededTransactions[playerId] += amountUAH;
                return true;
            }
            else
            {
                if (amountUAH > Limit)
                    return false;
                
                _proceededTransactions.Add(playerId, amountUAH);
                return true;
            }
        }

        protected virtual bool IsTransactionAmountValid(decimal amount, string currency)
        {
            return true;
        }
        
        
        
        private void ShowAvailableCards()
        {
            for (int i = 0; i < AvailableCards.Length; i++)
            {
                Console.WriteLine($"{i}. {AvailableCards[i]}");
            }
        }
    }
}