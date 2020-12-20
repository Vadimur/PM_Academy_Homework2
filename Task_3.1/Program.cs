using System;
using Library;
namespace Task_3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var creditCard = new CreditCard();
            creditCard.StartDeposit(0, 50, "USD"); // success, with valid Mastercard number
            
            
            creditCard.StartWithdrawal(1, 50, "USD"); // failed, with invalid card number
            
            
            creditCard.StartWithdrawal(2, 50, "USD"); // success, with valid Visa card number
            
            
            var privet48 = new Privet48();
            privet48.StartDeposit(3, 50, "USD"); // success, with Gold card
            
            
            var stereobank = new Stereobank();
            stereobank.StartWithdrawal(4, 50, "USD"); // success, with White card
            
            
            var giftVoucher = new GiftVoucher();
            giftVoucher.StartDeposit(5, 50, "USD"); // failed
            
            
            giftVoucher.StartDeposit(6, 500, "USD"); // failed, with invalid voucher number
            
            
            giftVoucher.StartDeposit(7, 500, "USD"); // success, with valid voucher number

        }
    }
}