namespace Abp.Runtime.Validation
{
    /// <summary>
    /// This interface is used to normalize inputs before method execution.
    /// �������ӿڣ��˽ӿ������ڷ���ִ��ǰ�淶����
    /// </summary>
    /// <remarks>
    /// �ýӿڶ�����Normalize������ʵ�ָ÷���������Validation ��ʹ��ǰ����DTO�����Ĵ���
    /// </remarks>
    public interface IShouldNormalize
    {
        /// <summary>
        /// This method is called lastly before method execution (after validation if exists).
        /// �淶������
        /// ������������ķ���ǰִ�У���֤��������ڣ���
        /// </summary>
        void Normalize();
    }
}