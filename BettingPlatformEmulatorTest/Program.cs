using System;
using Library;

namespace BettingPlatformEmulatorTest
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