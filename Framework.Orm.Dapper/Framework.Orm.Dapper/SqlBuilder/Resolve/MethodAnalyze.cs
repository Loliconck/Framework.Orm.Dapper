namespace Framework.Orm.Dapper.SqlBuilder.Resolve
{
    internal delegate void AddParameHandler(string name, object value);
    internal delegate string MethodHandler(string field, string paramName, AddParameHandler addParame, params object[] args);

    internal class MethodAnalyze
    {
        public string Contains(string field, string paramName, AddParameHandler addParame, object[] args)
        {
            addParame(paramName, args[0]);

            var result = string.Format("{0} LIKE '%'+{1}+'%'", field, paramName);
            return result;
        }

        public string StartsWith(string field, string paramName, AddParameHandler addParame, object[] args)
        {
            addParame(paramName, args[0]);

            var result = string.Format("{0} LIKE {1}+'%'", field, paramName);

            return result;
        }

        public string EndsWith(string field, string paramName, AddParameHandler addParame, object[] args)
        {
            addParame(paramName, args[0]);

            var result = string.Format("{0} LIKE '%'+{1}", field, paramName);

            return result;
        }
    }
}
