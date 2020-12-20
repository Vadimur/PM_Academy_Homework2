using System;
using System.Collections.Generic;
using Library;

namespace Task_1._2
{
    class Program
    {
        private static Account[] accounts;
        static void Main(string[] args)
        {
            int n = 1000000;
            accounts = new Account[n];
            for (int i = 0; i < n; i++)
            {
                accounts[i] = new Account(UniqueIdGenerator.GenerateUniqueId(), "UAH");
            }

            var sortedAccounts = GetSortedAccounts();
            Console.WriteLine("First ten accounts are:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(sortedAccounts[i].Id);
            }
            Console.WriteLine("\nLast ten accounts are:");
            for (int i = n - 10; i < n; i++)
            {
                Console.WriteLine(sortedAccounts[i].Id);
            }
        }

        private static Account[] GetSortedAccounts()
        {
            int n = accounts.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    if (accounts[j].Id > accounts[j+1].Id)
                    {
                        var temp = accounts[j+1];
                        accounts[j+1] = accounts[j];
                        accounts[j] = temp;
                    }
                }
            }

            return accounts;
        }
    }
}