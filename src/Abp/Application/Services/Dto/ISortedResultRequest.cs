namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a sorted result.
    /// 排序结果请求接口
    /// </summary>
    public interface ISortedResultRequest
    {
        /// <summary>
        /// Sorting information.
        /// Should include sorting field and optionally a direction (ASC or DESC)
        /// Can contain more than one field separated by comma (,).
        /// 排序信息
        /// 应包括排序字段和可选的方向（升序或降序）
        /// 可以包含多个字段用逗号分隔（，）
        /// </summary>
        /// <example>
        /// Examples:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// 例如：
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC" 
        /// </example>
        string Sorting { get; set; }
    }
}