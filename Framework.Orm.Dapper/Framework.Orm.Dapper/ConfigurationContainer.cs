using System.Reflection;

namespace Framework.Orm.Dapper.Core
{
    /// <summary>
    /// 相关配置管理容器
    /// </summary>
    public class ConfigurationContainer
    {
        internal static ConnectionStringManager ConnectionStringManager;
        internal static EntityInfoManager EntityInfoManager;
        public static int SingleMaxInsertCount
        {
            get
            {
                return 1000;
            }
        }

        static ConfigurationContainer()
        {
            EntityInfoManager = new EntityInfoManager();
        }

        /// <summary>
        /// Dapper初始化
        /// </summary>
        public static void Init()
        {
            ConnectionStringManager = new ConnectionStringManager();
            EntityInfoManager.Initialize(Assembly.GetExecutingAssembly());
        }
    }
}
