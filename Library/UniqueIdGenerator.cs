using System;
using System.Collections.Generic;

namespace Library
{
    public static class UniqueIdGenerator
    {
        static HashSet<int> _identificators = new HashSet<int>();

        public static int GenerateUniqueId()
        {
            Random random = new Random();
            int generatedId = 0;
            do
            {
                generatedId = random.Next(100000, 100000000);
            } while (_identificators.Contains(generatedId));

            return generatedId;
        }
    }
}