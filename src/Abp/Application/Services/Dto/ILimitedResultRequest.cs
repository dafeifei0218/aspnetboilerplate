namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a limited result.
    /// ���޽����������ӿڱ�����Ϊ��׼������Ҫ��һ�����޵Ľ����
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// Max expected result count.
        /// �������
        /// </summary>
        int MaxResultCount { get; set; }
    }
}