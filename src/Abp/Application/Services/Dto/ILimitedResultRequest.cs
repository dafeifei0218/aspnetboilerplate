namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a limited result.
    /// 有限结果请求，这个接口被定义为标准化，以要求一个有限的结果。
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// Max expected result count.
        /// 最大结果数
        /// </summary>
        int MaxResultCount { get; set; }
    }
}