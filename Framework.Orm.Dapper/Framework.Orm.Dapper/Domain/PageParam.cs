namespace Framework.Orm.Dapper.Domain
{
    /// <summary>
    /// 分页查询类
    /// </summary>
    public class PageParam
    {
        public PageParam()
        {
            PageIndex = 1;
            PageSize = 15;
            IsDesc = true;
        }

        /// <summary>
        /// 分页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (DataCount / PageSize) + ((DataCount % PageSize) > 0 ? 1 : 0);
            }
        }

        /// <summary>
        /// 开始行
        /// </summary>
        public int BeginRow
        {
            get { return (PageIndex - 1) * PageSize + 1; }
        }

        /// <summary>
        /// 结束行
        /// </summary>
        public int EndRow
        {
            get { return PageIndex * PageSize; }
        }

        /// <summary>
        /// 是否根据ID降序
        /// </summary>
        public bool IsDesc
        {
            get;
            set;
        }
    }
}
