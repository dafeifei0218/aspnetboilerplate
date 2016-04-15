using Abp.Dependency;
using Abp.Domain.Uow;

namespace Abp.MemoryDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMemoryDatabaseProvider"/> that gets database from active unit of work.
    /// ������Ԫ�ڴ����ݿ��ṩ�ߡ�
    /// </summary>
    public class UnitOfWorkMemoryDatabaseProvider : IMemoryDatabaseProvider, ITransientDependency
    {
        /// <summary>
        /// �ڴ����ݿ�
        /// </summary>
        public MemoryDatabase Database { get { return ((MemoryDbUnitOfWork)_currentUnitOfWork.Current).Database; } }

        /// <summary>
        /// ��ǰ������Ԫ�ṩ��
        /// </summary>
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="currentUnitOfWork">��ǰ������Ԫ</param>
        public UnitOfWorkMemoryDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}