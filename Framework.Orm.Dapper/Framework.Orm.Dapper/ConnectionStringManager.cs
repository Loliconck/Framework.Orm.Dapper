using System;
using System.Collections.Generic;
using System.Configuration;

namespace Framework.Orm.Dapper
{
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

        /// <summary>
        /// 根据KEY获取连接字符串
        /// </summary>
        /// <param name="key">连接字符串的KEY</param>
        /// <returns>连接字符串</returns>
        public string GetConnectionString(string key)
        {
            return this[key];
        }

        /// <summary>
        /// 新增连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionString"></param>
        public void AddConnectionString(string key, string connectionString)
        {
            if (!string.IsNullOrEmpty(key))
            {
                this[key] = connectionString;
            }
        }

        public string GetDefaultKey()
        {
            if ((ConnectionStringsDic == null || ConnectionStringsDic.Count <= 0))
                return string.Empty;
            foreach (var key in ConnectionStringsDic.Keys)
            {
                if (!String.Equals(key, "LocalSqlServer"))
                {
                    return key;
                }
            }
            return string.Empty;
        }
    }
}
