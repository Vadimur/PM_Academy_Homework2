using System;

namespace Library.Exceptions
{
    public class PaymentServiceException : Exception
    {
        public PaymentServiceException()
        {
            
        }

        public PaymentServiceException(string message) : base(message)
        {
            
        }
    }
}