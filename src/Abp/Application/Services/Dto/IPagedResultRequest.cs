namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a paged result.
    /// ��ҳ�������ӿ�
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        /// <summary>
        /// Skip count (beginning of the page).
        /// ����������ҳ��ͷ��
        /// </summary>
        int SkipCount { get; set; }
    }
}