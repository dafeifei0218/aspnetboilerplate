using System.Data.Entity;
using Abp.Domain.Uow;

namespace Abp.EntityFramework.Uow
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from
    /// active unit of work.
    /// 工作单元数据上下文提供者
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext> 
        where TDbContext : DbContext
    {
        /// <summary>
        /// Gets the DbContext.
        /// 获取数据上下文
        /// </summary>
        public TDbContext DbContext { get { return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(); } }

        /// <summary>
        /// 当前工作单元提供者
        /// </summary>
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkDbContextProvider{TDbContext}"/>.
        /// 构造函数
        /// </summary>
        /// <param name="currentUnitOfWorkProvider">当前工作单元提供者</param>
        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }
    }
}