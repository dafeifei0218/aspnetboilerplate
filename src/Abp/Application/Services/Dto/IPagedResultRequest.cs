namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a paged result.
    /// 分页结果请求接口
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        /// <summary>
        /// Skip count (beginning of the page).
        /// 跳过数量（页开头）
        /// </summary>
        int SkipCount { get; set; }
    }
}