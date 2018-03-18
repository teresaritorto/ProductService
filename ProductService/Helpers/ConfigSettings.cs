using System;
using System.Configuration;

namespace ProductService.Helpers
{
    /// <summary>
    /// Configuration Settings Helper
    /// </summary>
    public static class ConfigSettings
    {
        /// <summary>
        /// Redis Server Config
        /// </summary>
        public static string RedisServer
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisServer"] ?? "localhost";
            }
        }

        /// <summary>
        /// Redis Port Config
        /// </summary>
        public static int RedisPort
        {
            get
            {
                int port;
                var config = ConfigurationManager.AppSettings["RedisPort"] ?? "6379";

                if (!int.TryParse(config, out port))
                {
                    throw new InvalidOperationException("Invalid RedisPort in web.config");
                }

                return port;
            }
        }
    }
}