namespace SupermarketChain.Common.Utils
{
    using System;
    using System.Configuration;

    public static class ConfigUtils
    {
        public static string GetAppSetting(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new FormatException("Key can not be null or empty");
            }

            return ConfigurationManager.AppSettings[key];
        }

        public static string GetConnectionString(string name)
        {
            ConnectionStringSettings connectionStringSettings = ConfigUtils.GetConnectionStringSettings(name);

            return connectionStringSettings.ConnectionString;
        }

        public static ConnectionStringSettings GetConnectionStringSettings(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new FormatException("Connection string name can not be null or empty");
            }

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[name];

            return connectionStringSettings;
        }
    }
}
