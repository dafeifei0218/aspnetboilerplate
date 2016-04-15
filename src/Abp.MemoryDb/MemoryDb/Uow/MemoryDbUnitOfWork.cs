using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MemoryDb.Configuration;

namespace Abp.MemoryDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MemoryDb.
    /// �ڴ����ݿ⹤����Ԫ��
    /// </summary>
    public class MemoryDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to Memory Database.
        /// ��ȡ�ڴ����ݿ�
        /// </summary>
        public MemoryDatabase Database { get; private set; }

        /// <summary>
        /// �ڴ����ݿ�ģ������
        /// </summary>
        private readonly IAbpMemoryDbModuleConfiguration _configuration;
        /// <summary>
        /// �ڴ����ݿ�
        /// </summary>
        private readonly MemoryDatabase _memoryDatabase;

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        public MemoryDbUnitOfWork(IAbpMemoryDbModuleConfiguration configuration, MemoryDatabase memoryDatabase, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _configuration = configuration;
            _memoryDatabase = memoryDatabase;
        }

        /// <summary>
        /// ��ʼ������Ԫ
        /// </summary>
        protected override void BeginUow()
        {
            Database = _memoryDatabase;
        }

        /// <summary>
        /// ������
        /// </summary>
        public override void SaveChanges()
        {

        }

        /// <summary>
        /// ������-�첽
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {

        }

        /// <summary>
        /// ��ɹ�����Ԫ
        /// </summary>
        protected override void CompleteUow()
        {

        }

        /// <summary>
        /// ��ɹ�����Ԫ-�첽
        /// </summary>
        /// <returns></returns>
        protected override async Task CompleteUowAsync()
        {

        }

        /// <summary>
        /// ���ٹ�����Ԫ
        /// </summary>
        protected override void DisposeUow()
        {

        }
    }
}