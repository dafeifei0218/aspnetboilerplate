using System.Threading.Tasks;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Null implementation of unit of work.
    /// It's used if no component registered for <see cref="IUnitOfWork"/>.
    /// This ensures working ABP without a database.
    /// 工作单元空实现
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public async override Task SaveChangesAsync()
        {

        }

        protected override void BeginUow()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected async override Task CompleteUowAsync()
        {

        }

        protected override void DisposeUow()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defaultOptions">工作单元默认选项</param>
        public NullUnitOfWork(IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
        }
    }
}
