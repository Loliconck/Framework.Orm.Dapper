using System.Reflection;

namespace Framework.Orm.Dapper.Core
{
    public class ConfigurationContainer
    {
        internal static ConnectionStringManager ConnectionStringManager;
        internal static EntityInfoManager EntityInfoManager;

        static ConfigurationContainer()
        {
            ConnectionStringManager = new ConnectionStringManager();
            EntityInfoManager = new EntityInfoManager();
        }

        public static void Init()
        {
            EntityInfoManager.Initialize(Assembly.GetExecutingAssembly());
        }
    }
}
