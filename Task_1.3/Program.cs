using System;
using System.Collections.Generic;
using Library;

namespace Task_1._3
{
    class Program
    {
        private static Account[] accounts;
        static void Main(string[] args)
        {
            int size = 1000000;
            accounts = new Account[size];
            for (int i = 0; i < size; i++)
            {
                accounts[i] = new Account(UniqueIdGenerator.GenerateUniqueId(), "UAH");
            }
            Console.WriteLine("Enter id to find: ");
            
            
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput,out int id) == false)
                Console.WriteLine("Invalid input");
            else
            {
                GetSortedAccounts();
                GetAccount(id);
            }
        }

        private static void GetAccount(int id)
        {
            int counter = 0;
            int leftBorder = 0;
            int rightBorder = accounts.Length - 1;
            while (leftBorder <= rightBorder)
            {
                counter++;
                int mid = (rightBorder + leftBorder) / 2;
                if (accounts[mid].Id == id)
                {
                    Console.WriteLine($"{id} was found at index {++mid} by {counter} tries");
                    return;
                }
                
                if (accounts[mid].Id > id)
                {
                    rightBorder = mid - 1;
                }
                else
                {
                    leftBorder = mid + 1;
                }
            }
            

            Console.WriteLine($"There is no account {id} in the list");
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