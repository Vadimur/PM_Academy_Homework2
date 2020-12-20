using System;
using System.Collections.Generic;
using Library;

namespace Task_1._5
{
    class Program
    {
        static HashSet<int> _identificators = new HashSet<int>(); // accounts and players can't have the same ID
        static void Main(string[] args)
        {
            Player  player1 = new Player(UniqueIdGenerator.GenerateUniqueId(), "John Doe", "Betman", 
                                        "john777@gmail.com", "TheP@$$w0rd","USD");


            string validPassword = "TheP@$$w0rd";
            bool isLoginValid = player1.IsPasswordValid(validPassword);
            Console.WriteLine($"Login with login {player1.Email} and password {validPassword} is {isLoginValid}");
            
            string invalidPassword = "bad password";
            isLoginValid = player1.IsPasswordValid(invalidPassword);
            Console.WriteLine($"Login with login {player1.Email} and password {invalidPassword} is {isLoginValid}");
            
            player1.Deposit(100, "USD");
            player1.Withdraw(50, "EUR");
            
            try
            {
                player1.Withdraw(1000, "USD");
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
                Player  player2 = new Player(UniqueIdGenerator.GenerateUniqueId(), "John Doe", "Betman", 
                    "john777@gmail.com", "TheP@$$w0rd", "PLN");
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

    }
}