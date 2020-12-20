using System;

namespace Library.Exceptions
{
    public class LimitExceededException : PaymentServiceException
    {
        public LimitExceededException()
        {
            
        }

        public LimitExceededException(string message) : base(message)
        {
            
        }
    }
}