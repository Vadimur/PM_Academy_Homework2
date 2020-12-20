using System;
using Library;

namespace Task_3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService paymentService = new PaymentService();
            
            paymentService.StartDeposit(0, 50, "USD"); // success, with valid CreditCard Mastercard number
            
            
            paymentService.StartWithdrawal(1, 50, "USD"); // failed, with invalid CreditCard card number
            
            
            paymentService.StartWithdrawal(2, 50, "USD"); // success, with valid CreditCard Visa card number

            
            paymentService.StartDeposit(3, 50, "USD"); // success, with Privet48 Gold card
            
            
            paymentService.StartWithdrawal(4, 50, "USD"); // success, with Stereobank White card
            
            
            paymentService.StartDeposit(5, 50, "USD"); // failed with GiftVoucher
            
            
            paymentService.StartDeposit(6, 500, "USD"); // failed, with invalid GiftVoucher number
            
            
            paymentService.StartDeposit(7, 500, "USD"); // success, with valid GiftVoucher number
        }
    }
}