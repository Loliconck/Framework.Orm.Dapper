using System.Reflection;

namespace Framework.Orm.Dapper.Core
{
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
            ConnectionStringManager = new ConnectionStringManager();
            EntityInfoManager = new EntityInfoManager();
        }

        public static void Init()
        {
            EntityInfoManager.Initialize(Assembly.GetExecutingAssembly());
        }
    }
}
