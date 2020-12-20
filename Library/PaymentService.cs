using System;
using Library.Exceptions;

namespace Library
{
    public class PaymentService
    {
        private readonly PaymentMethodBase[] AvailablePaymentMethod;

        public PaymentService()
        {
            AvailablePaymentMethod = new PaymentMethodBase[]
            {
                new CreditCard(), 
                new Privet48(), 
                new Stereobank(), 
                new GiftVoucher()
            };
        }

        public void StartDeposit(int playerId, decimal amount, string currency)
        {
            PrintPaymentServicesForDeposit();
            do
            {
                string userInput = Console.ReadLine();
                
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Empty input. Try again\n");
                    continue;
                }
                if (int.TryParse(userInput, out int depositProviderId) == false)
                {
                    Console.WriteLine("Unknown provider ID. Try again\n");
                    continue;
                }
                bool isProviderValid = ProcessDepositCommand(depositProviderId, playerId, amount, currency);
                
                if (isProviderValid == false)
                {
                    Console.WriteLine("Unknown provider ID. Try again\n");
                }
                Console.WriteLine("");
                break;
                
            } while (true);
        }

        public void StartWithdrawal(int playerId, decimal amount, string currency)
        {
            PrintPaymentServicesForWithdrawal();
            do
            {
                string userInput = Console.ReadLine();
                
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Empty input. Try again\n");
                    continue;
                }
                if (int.TryParse(userInput, out int withdrawalProviderId) == false)
                {
                    Console.WriteLine("Unknown provider ID. Try again\n");
                    continue;
                }
                bool isProviderValid = ProcessWithdrawalCommand(withdrawalProviderId, playerId, amount, currency);
                
                if (isProviderValid == false)
                {
                    Console.WriteLine("Unknown provider ID. Try again\n");
                }
                Console.WriteLine("");
                break;
                
            } while (true);
        }

        private bool ProcessDepositCommand(int depositProviderId, int playerId, decimal amount, string currency)
        {
            ISupportDeposit depositProvider;
            switch (depositProviderId)
            {
                case 1:
                    depositProvider = (ISupportDeposit)AvailablePaymentMethod[0];
                    break;
                case 2:
                    depositProvider = (ISupportDeposit)AvailablePaymentMethod[1];
                    break;
                case 3:
                    depositProvider = (ISupportDeposit)AvailablePaymentMethod[2];
                    break;
                case 4:
                    depositProvider = (ISupportDeposit)AvailablePaymentMethod[3];
                    break;
                default:
                    return false;
            }
            
            Random random = new Random();
            int number = random.Next(1, 101);
            if (number <= 2)
            { 
                throw new PaymentServiceException();
            }
            depositProvider.StartDeposit(playerId, amount, currency);
            return true;
        }
        
        private bool ProcessWithdrawalCommand(int withdrawalProviderId, int playerId, decimal amount, string currency)
        {
            ISupportWithdrawal withdrawalProvider;
            switch (withdrawalProviderId)
            {
                case 1:
                    withdrawalProvider = (ISupportWithdrawal)AvailablePaymentMethod[0];
                    break;
                case 2:
                    withdrawalProvider = (ISupportWithdrawal)AvailablePaymentMethod[1];
                    break;
                case 3:
                    withdrawalProvider = (ISupportWithdrawal)AvailablePaymentMethod[2];
                    break;
                default:
                    return false;
            }
           
            Random random = new Random();
            int number = random.Next(1, 101);
            if (number <= 2)
            {
                throw new PaymentServiceException();
            }

            withdrawalProvider.StartWithdrawal(playerId, amount, currency);
            return true;
        }
        
        private void PrintPaymentServicesForDeposit()
        {
            Console.WriteLine();
            Console.WriteLine("1. CreditCard");
            Console.WriteLine("2. Privet48");
            Console.WriteLine("3. Stereobank");
            Console.WriteLine("4. GiftVoucher");
        }
        
        private void PrintPaymentServicesForWithdrawal()
        {
            Console.WriteLine();
            Console.WriteLine("1. CreditCard");
            Console.WriteLine("2. Privet48");
            Console.WriteLine("3. Stereobank");
        }
    }
}