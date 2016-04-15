namespace Abp.MemoryDb
{
    /// <summary>
    /// Defines interface to obtain a <see cref="MemoryDatabase"/> object.
    /// �ڴ����ݿ��ṩ�߽ӿ�
    /// </summary>
    public interface IMemoryDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="MemoryDatabase"/>.
        /// ��ȡ�ڴ����ݿ�
        /// </summary>
        MemoryDatabase Database { get; }
    }
}