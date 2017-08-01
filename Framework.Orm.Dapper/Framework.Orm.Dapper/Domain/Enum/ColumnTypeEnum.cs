namespace Framework.Orm.Dapper.Domain.Enum
{
    public enum ColumnTypeEnum
    {
        None,
        /// <summary>
        /// 仅仅插入时操作，不可修改
        /// </summary>
        Insert,
        /// <summary>
        /// 需要强制更新的
        /// </summary>
        Update,
        /// <summary>
        /// 标识删除逻辑字段
        /// </summary>
        Delete
    }
}
