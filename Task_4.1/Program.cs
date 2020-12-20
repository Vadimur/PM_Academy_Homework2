using System;
using Library.Exceptions;
namespace Task_4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new LimitExceededException();
            }
            catch (InsufficientFundsException exception)
            {
                Console.WriteLine(exception.GetType());
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine(exception.GetType());
            }
            catch (PaymentServiceException exception)
            {
                Console.WriteLine(exception.GetType());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.GetType());
            }
        }
    }
}