using System;
using System.Collections.Generic;
using System.Globalization;
using Library.Exceptions;

namespace Library
{
    public class BettingPlatformEmulator
    {
        private readonly List<Player> Players;
        private Player ActivePlayer { get; set; }
        private readonly Account PlatformAccount;
        private readonly BetService _betService;
        private readonly PaymentService _paymentService;

        public BettingPlatformEmulator()
        {
            Players = new List<Player>();
            PlatformAccount = new Account(UniqueIdGenerator.GenerateUniqueId(), "USD");
            _betService = new BetService();
            _paymentService = new PaymentService();
        }
        
        public void Start()
        {
            bool keepProgramActive = true;
            do
            {
                PrintUserMenu();
                Console.Write("Enter command: ");
                string userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Empty input. Try again\n");
                    continue;
                }

                if (int.TryParse(userInput, out int commandId) == false)
                {
                    Console.WriteLine("Unknown command id. Try again\n");
                    continue;
                }

                bool isCommandValid = ChooseCommand(commandId);
                if (isCommandValid == false)
                {
                    Console.WriteLine("Unknown command id. Try again\n");
                }
            } while (keepProgramActive);
            
        }

        private bool ChooseCommand(int commandNumber)
        {
            bool correctCommand = true;
            if (ActivePlayer == null)
            {
                switch (commandNumber)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        Login();
                        break;
                    case 3:
                        Exit();
                        break;
                    default:
                        correctCommand = false;
                        break;
                }
            }
            else
            {
                switch (commandNumber)
                {
                    case 1:
                        Deposit();
                        break;
                    case 2:
                        Withdraw();
                        break;
                    case 3:
                        GetOdds();
                        break;
                    case 4:
                        Bet();
                        break;
                    case 5:
                        Logout();
                        break;
                    default:
                        correctCommand = false;
                        break;
                }
            }
            return correctCommand;
        }

        private void Exit()
        {
            Environment.Exit(0);
        }

        private void Register()
        {
            Console.WriteLine("Enter your name, please");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter your last name, please");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter your login, please");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password, please");
            string password = Console.ReadLine();
            bool isValidCurrency = false;
            do
            {
                Console.WriteLine("Enter your base currency, please");
                var currency = Console.ReadLine();
                if (string.IsNullOrEmpty(currency))
                    continue;
                
                try
                {
                    Player newPlayer = new Player(UniqueIdGenerator.GenerateUniqueId(), firstName, lastName,
                        email, password, currency);
                    Players.Add(newPlayer);
                    isValidCurrency = true;
                    Console.WriteLine("Account successfully registered");
                }
                catch (NotSupportedException exception)
                {
                    Console.WriteLine(exception.Message);
                }

            } while (isValidCurrency == false);
        }

        private void Login()
        {
            Console.WriteLine("Enter your login, please");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password, please");
            string password = Console.ReadLine();
            foreach (var player in Players)
            {
                if (player.Email.Equals(email) && player.IsPasswordValid(password))
                {
                    ActivePlayer = player;
                    break;
                }
            }

            if (ActivePlayer == null)
            {
                Console.WriteLine($"Unable to login into {email}. Incorrect login or password.");
            }
            else
            {
                Console.WriteLine($"Logged in {ActivePlayer.Email}");

            }
        }

        private void Logout()
        {
            ActivePlayer = null;
            Console.WriteLine("Logged out");

        }

        private void Deposit()
        {
            Console.WriteLine("Enter the amount of your deposit, please");
            string amount = Console.ReadLine()?.Replace(",", ".");
            if (decimal.TryParse(amount,NumberStyles.Any, CultureInfo.InvariantCulture, out decimal moneyAmount) == false || moneyAmount <= 0)
            {
                Console.WriteLine("Invalid money amount");
                return;
            }
            Console.WriteLine("Enter currency, please");
            string currency = Console.ReadLine();
            try
            {
                _paymentService.StartDeposit(ActivePlayer.Id, moneyAmount, currency);
                Console.WriteLine("Deposit was made");
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount");
                return;
            }
            catch (InsufficientFundsException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
                return;
            }
            catch (PaymentServiceException exception)
            {
                Console.WriteLine("Something went wrong. Try again later..");
                return;
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException ||
                    exception is NotSupportedException)
                {
                    Console.WriteLine(exception.Message);
                    return;
                }
                
                throw;
            }
            
            try
            {                
                ActivePlayer.Deposit(moneyAmount, currency);
                PlatformAccount.Deposit(moneyAmount, currency);
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException ||
                    exception is NotSupportedException)
                {
                    Console.WriteLine(exception.Message);
                    return;
                }
                
                throw;
            }
        }

        private void Withdraw()
        {
            Console.WriteLine("Enter the amount of your withdrawal, please");
            string amount = Console.ReadLine()?.Replace(",", ".");
            if (decimal.TryParse(amount,NumberStyles.Any, CultureInfo.InvariantCulture, out decimal moneyAmount) == false || moneyAmount <= 0)
            {
                Console.WriteLine("Invalid money amount");
                return;
            }
            Console.WriteLine("Enter currency, please");
            string currency = Console.ReadLine();

            bool userHasEnough = false;
            try
            {
                ActivePlayer.Withdraw(moneyAmount, currency);
                userHasEnough = true;
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException)
                {
                    Console.WriteLine("There is insufficient funds on your account");
                    return;
                }
                else if (exception is NotSupportedException)
                {                    
                    Console.WriteLine(exception.Message);
                    return;
                }
                else
                    throw;
            }
            
            try
            {
                if (userHasEnough)
                    PlatformAccount.Withdraw(moneyAmount, currency);
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException)
                {
                    Console.WriteLine("There is some problem on the platform side. Please try it later");
                    ActivePlayer.Deposit(moneyAmount, currency);
                    return;
                }
                else if (exception is NotSupportedException)
                {                    
                    Console.WriteLine(exception.Message);
                    ActivePlayer.Deposit(moneyAmount, currency);
                    return;
                }
                else
                    throw;
            }
            
            
            try
            {
                _paymentService.StartWithdrawal(ActivePlayer.Id, moneyAmount, currency); 
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount");
                ActivePlayer.Deposit(moneyAmount, currency);
                PlatformAccount.Deposit(moneyAmount, currency);
                return;
            }
            catch (InsufficientFundsException exception)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
                ActivePlayer.Deposit(moneyAmount, currency);
                PlatformAccount.Deposit(moneyAmount, currency);
                return;
            }
            catch (PaymentServiceException exception)
            {
                Console.WriteLine("Something went wrong. Try again later..");
                ActivePlayer.Deposit(moneyAmount, currency);
                PlatformAccount.Deposit(moneyAmount, currency);
                return;
            }
        }

        private void GetOdds()
        {
            var odd = _betService.GetOdds();
            Console.WriteLine($"Current odd is {odd}");
        }

        private void Bet()
        {
            Console.WriteLine("Enter the amount of your bet, please");
            string rawBet = Console.ReadLine()?.Replace(",", ".");
            if (decimal.TryParse(rawBet, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal bet) == false || bet < 0)
            {
                Console.WriteLine("Invalid money amount");
                return;
            }
            Console.WriteLine("Enter currency, please");
            string currency = Console.ReadLine();
            try
            {
                ActivePlayer.Withdraw(bet, currency);
                var betInUsd = CurrencyExchanger.ConvertCurrency(currency, "USD", bet);
                var prize = _betService.Bet(betInUsd);
                if (prize > 0 )
                {
                    ActivePlayer.Deposit(prize, "USD");
                    Console.WriteLine($"Congrats! You won {prize}");
                }
                else
                {
                    Console.WriteLine("You lost");
                }
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException)
                    Console.WriteLine("There is insufficient funds on your account");
                else if (exception is NotSupportedException)
                    Console.WriteLine(exception.Message);
                else
                    throw;
            }

        }
        private void PrintUserMenu()
        {
            if (ActivePlayer == null)
                PrintInactiveUserMenu();
            else
                PrintActiveUserMenu();
        }
        
        private static void PrintInactiveUserMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Stop");
        }
        
        private static void PrintActiveUserMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Get Odds");
            Console.WriteLine("4. Bet");
            Console.WriteLine("5. Logout");       
        }
    }
}