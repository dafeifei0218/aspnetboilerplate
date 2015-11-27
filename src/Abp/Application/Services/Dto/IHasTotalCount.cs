namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO.
    /// 项目总数接口
    /// </summary>
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// 项目总数
        /// </summary>
        int TotalCount { get; set; }
    }
}