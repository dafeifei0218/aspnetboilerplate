using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MemoryDb.Configuration;

namespace Abp.MemoryDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MemoryDb.
    /// 内存数据库工作单元。
    /// </summary>
    public class MemoryDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to Memory Database.
        /// 获取内存数据库
        /// </summary>
        public MemoryDatabase Database { get; private set; }

        /// <summary>
        /// 内存数据库模块配置
        /// </summary>
        private readonly IAbpMemoryDbModuleConfiguration _configuration;
        /// <summary>
        /// 内存数据库
        /// </summary>
        private readonly MemoryDatabase _memoryDatabase;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public MemoryDbUnitOfWork(IAbpMemoryDbModuleConfiguration configuration, MemoryDatabase memoryDatabase, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _configuration = configuration;
            _memoryDatabase = memoryDatabase;
        }

        /// <summary>
        /// 开始工作单元
        /// </summary>
        protected override void BeginUow()
        {
            Database = _memoryDatabase;
        }

        /// <summary>
        /// 保存变更
        /// </summary>
        public override void SaveChanges()
        {

        }

        /// <summary>
        /// 保存变更-异步
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {

        }

        /// <summary>
        /// 完成工作单元
        /// </summary>
        protected override void CompleteUow()
        {

        }

        /// <summary>
        /// 完成工作单元-异步
        /// </summary>
        /// <returns></returns>
        protected override async Task CompleteUowAsync()
        {

        }

        /// <summary>
        /// 销毁工作单元
        /// </summary>
        protected override void DisposeUow()
        {

        }
    }
}