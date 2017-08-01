namespace Framework.Orm.Dapper.SqlBuilder.Infrastructure
{
    /// <summary>
    /// 参数列名和SQL字段的对应
    /// </summary>
    public class ParamColumnModel
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 对应类属性名
        /// </summary>
        public string FieldName { get; set; }
    }
}
