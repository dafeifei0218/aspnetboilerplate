namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO for long type.
    /// ��������Ŀ�����ӿ�
    /// </summary>
    public interface IHasLongTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// ��Ŀ����
        /// </summary>
        long TotalCount { get; set; }
    }
}