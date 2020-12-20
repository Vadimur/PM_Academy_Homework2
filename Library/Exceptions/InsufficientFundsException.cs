using System;

namespace Library.Exceptions
{
    public class InsufficientFundsException : PaymentServiceException
    {
        public InsufficientFundsException()
        {
            
        }

        public InsufficientFundsException(string message) : base(message)
        {
            
        }
    }
}