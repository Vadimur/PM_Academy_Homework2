using System;
using Library;

namespace Task_2._1
{
    class Program
    {
        private static BetService _betService;
        static void Main(string[] args)
        {
            _betService = new BetService();
            
            RandomOdd10Times();
            ThreeBets100Usd();
            TestAlgorithm();
        }

        
        private static void RandomOdd10Times()
        {
            for (int i = 0; i < 9; i++)
            {
                _betService.GetOdds(); // 9 times
            }

            var odd = _betService.GetOdds(); // 10 times 
            var prize = _betService.Bet(100);
            Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {prize}\n");
        }
        
        private static void ThreeBets100Usd()
        {
            int counter = 0;
            do
            {
                var odd = _betService.GetOdds();
                if (odd > 12)
                {
                    var prize = _betService.Bet(100);
                    Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {prize}");
                    counter++;
                }

            } while (counter < 3);
        }

        private static void TestAlgorithm()
        {
            Console.WriteLine("\n\nTesting algorithm");
            decimal balance = 10000m;
            Random random = new Random();
            while (balance > 0 && balance < 150000)
            {
                var odd = _betService.GetOdds();
                if (odd >= 1.5)
                {
                    decimal bet = random.Next(0, 101);
                    if (bet > balance)
                        bet = balance;
                    balance -= bet;
                    var prize = _betService.Bet(bet);
                    balance += prize;
                }
            }

            Console.WriteLine($"Game is over. My balance is {balance}");
        }

    }
}