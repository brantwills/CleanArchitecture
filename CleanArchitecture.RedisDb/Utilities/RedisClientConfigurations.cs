using CleanArchitecture.RedisDb.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.RedisDb.Utilities
{
    public static class RedisClientConfigurations
    {
        public static string Url { get; set; } = ConfigurationHelper.Get("RedisServer", "127.0.0.1");
        public static int Port { get; set; } = 6379;
        public static int ConnectTimeout { get; set; } = 10000;
        public static int ConnectRetry { get; set; } = 3;
        public static int DefaultDatabase { get; set; } = ConfigurationHelper.Get("RedisDataBase", 0);
        public static bool PreserveAsyncOrder { get; set; } = false;
    }
}
