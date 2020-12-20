using System;
using Library;

namespace Task_2._2
{
    class Program
    {
        private static BettingPlatformEmulator _platform;
        static void Main(string[] args)
        {
            _platform = new BettingPlatformEmulator();
            _platform.Start();
        }
    }
}