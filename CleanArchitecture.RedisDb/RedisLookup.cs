using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.RedisDb
{
    public class RedisLookup
    {
        /// <summary>
        /// wip(bwills): sketching out how to reduce magic strings on redis items
        /// </summary>
        public class Customer
        {
            private static string HashKey = "customer:hash";

            private static string HashField = "customer:id";

            public static string GetHashKey() => HashKey;

            public static string GetHashField(int id) => $"{HashField}:{id}";
        }
    }
}
