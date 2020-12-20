using System;

namespace Library
{
    public class BetService
    {
        private readonly int minOdd = 101;
        private readonly int maxOdd = 2501;

        public decimal Odd { get; private set; }

        public BetService()
        {
            Random random = new Random();
            Odd = (decimal)random.Next(minOdd, maxOdd) / 100;
        }

        public float GetOdds()
        {
            Random random = new Random();
            Odd = (decimal)random.Next(minOdd, maxOdd) / 100;
            return (float) Odd;
        }

        public bool IsWon()
        {
            decimal winProbability = 100 / Odd;
            Random random = new Random();
            int result = random.Next(0, 100);
            
            return result <= winProbability;
        }

        public decimal Bet(decimal amount)
        {
            return IsWon() ? amount * Odd : 0;
        }
    }
}