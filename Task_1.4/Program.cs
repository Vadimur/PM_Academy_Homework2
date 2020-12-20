using System;
using System.Collections.Generic;
using Library;

namespace Task_1._4
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

            var sortedAccounts = GetSortedAccountsByQuickSort(accounts, 0, n - 1);
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
        
        private static Account[] GetSortedAccountsByQuickSort(Account []arr,int low, int high)
        {
            if (low < high) 
            {
                int pi = Partition(arr, low, high); 
                
                GetSortedAccountsByQuickSort(arr, low, pi-1); 
                GetSortedAccountsByQuickSort(arr, pi+1, high); 
            } 
            return accounts;
        }
        
        private static int Partition(Account[] arr, int low, int high) 
        { 
            int pivot = arr[high].Id;  
          
            int i = (low - 1);  
            for (int j = low; j < high; j++) 
            { 

                if (arr[j].Id < pivot) 
                { 
                    i++;
                     
                    Account temp = arr[i]; 
                    arr[i] = arr[j]; 
                    arr[j] = temp; 
                } 
            } 
  
            Account temp1 = arr[i+1]; 
            arr[i+1] = arr[high]; 
            arr[high] = temp1; 
  
            return i+1; 
        }
    }
}