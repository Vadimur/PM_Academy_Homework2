using System;
using System.Collections.Generic;
using Library.Exceptions;

namespace Library
{
    public class GiftVoucher : PaymentMethodBase, ISupportDeposit
    {
        private readonly int[] _voucherAmounts = new[] {100, 500, 1000};
        private readonly List<string> _usedVouchers;
        public GiftVoucher()
        {
            Name = "GiftVoucher";
            _usedVouchers = new List<string>();
        }
        public void StartDeposit(int playerId, decimal amount, string currency)
        {
            if (IsExistingVoucherAmount(amount) == false)
                throw  new NotSupportedException("You entered unavailable gift voucher amount. Try again later.");
            
            currency = currency.ToUpper();
            if (!currency.Equals("USD") && !currency.Equals("EUR") && !currency.Equals("UAH"))
                throw new NotSupportedException($"Not supported currency: {currency}");
            
            Console.WriteLine("Welcome, dear client!");
            string voucherCardId;
            do
            {
                Console.WriteLine("Please, enter gift voucher number");
                voucherCardId = Console.ReadLine();
            } while (!int.TryParse(voucherCardId, out int voucherId) || voucherCardId.Length != 10);

            if (IsVoucherAlreadyUsed(voucherCardId))
            {
                throw new InsufficientFundsException();
            }
            _usedVouchers.Add(voucherCardId);

            Console.WriteLine($"Your voucher with ID {voucherCardId} successfully used");
        }

        private bool IsExistingVoucherAmount(decimal voucherAmount)
        {
            foreach (var amount in _voucherAmounts)
            {
                if (amount == voucherAmount)
                    return true;
            }

            return false;
        }

        private bool IsVoucherAlreadyUsed(string voucherId)
        {
            return _usedVouchers.Contains(voucherId);
        }
    }
}