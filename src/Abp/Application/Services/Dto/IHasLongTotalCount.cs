namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO for long type.
    /// 长类型项目总数接口
    /// </summary>
    public interface IHasLongTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// 项目总数
        /// </summary>
        long TotalCount { get; set; }
    }
}