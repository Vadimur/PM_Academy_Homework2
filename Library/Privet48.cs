using System.Collections.Generic;

namespace Library
{
    public class Privet48 : Bank
    { 
        public Privet48()
        {
            Name = "Privet48";
            AvailableCards = new string[]{"Gold", "Platinum"};
            Limit = 10000;

        }
    }
}