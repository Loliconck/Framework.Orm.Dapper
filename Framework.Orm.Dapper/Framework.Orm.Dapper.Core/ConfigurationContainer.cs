using System.Collections.Generic;
using System.Configuration;

namespace Framework.Orm.Dapper.Core
{
    public class ConfigurationContainer
    {
        private static ConnectionStringManager connectionStrings;

        static ConfigurationContainer()
        {
            connectionStrings = new ConnectionStringManager();
        }

        /// <summary>
        /// 新增连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionString"></param>
        public static void AddConnectionString(string key, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionStrings[key]))
            {
                connectionStrings[key] = connectionString;
            }
        }

        /// <summary>
        /// 根据KEY获取连接字符串
        /// </summary>
        /// <param name="key">连接字符串的KEY</param>
        /// <returns>连接字符串</returns>
        public static string GetConnectionString(string key)
        {
            return connectionStrings[key];
        }
    }

    internal class ConnectionStringManager
    {
        private static Dictionary<string, string> ConnectionStringsDic = new Dictionary<string, string>();

        static ConnectionStringManager()
        {
            InitConnectionString();
        }

        private static void InitConnectionString()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            for (int i = 0; i < connectionStrings.Count; i++)
            {
                var key = connectionStrings[i].Name;
                var value = connectionStrings[i].ConnectionString;
                if (!ConnectionStringsDic.ContainsKey(key))
                {
                    ConnectionStringsDic.Add(key, value);
                }
            }
        }

        public string this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key) || !ConnectionStringsDic.ContainsKey(key))
                {
                    return string.Empty;
                }
                return ConnectionStringsDic[key];
            }
            set
            {
                if (!string.IsNullOrEmpty(key) && !ConnectionStringsDic.ContainsKey(key))
                {
                    ConnectionStringsDic[key] = value;
                }
            }
        }
    }
}
