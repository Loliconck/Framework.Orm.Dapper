using Framework.Orm.Dapper.Domain;
using System;
using System.Reflection;

namespace Framework.Orm.Dapper.Core
{
    /// <summary>
    /// 相关配置管理容器
    /// </summary>
    public class ConfigurationContainer
    {
        internal static ConnectionStringManager ConnectionStringManager;

        static ConfigurationContainer()
        {

        }

        /// <summary>
        /// Dapper初始化
        /// </summary>
        public static void Init()
        {
            ConnectionStringManager = new ConnectionStringManager();
            EntityInfoManager.Initialize(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 单次插入最大数量（超过500后启用批量插入)
        /// </summary>
        public static int SingleMaxInsertCount
        {
            get
            {
                return 1000;
            }
        }

        /// <summary>
        /// sqllog执行委托
        /// </summary>
        public static Action<string, object, long> DbLog = null;

        /// <summary>
        /// 业务日志委托
        /// </summary>
        public static Action<BusinessLog> BusinessLog = null;
    }
}
