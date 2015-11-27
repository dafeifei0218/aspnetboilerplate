namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO.
    /// ��Ŀ�����ӿ�
    /// </summary>
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// ��Ŀ����
        /// </summary>
        int TotalCount { get; set; }
    }
}