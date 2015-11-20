using System.Data.Entity;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// EF仓储基类
    /// </summary>
    /// <typeparam name="TDbContext">数据上下文</typeparam>
    /// <typeparam name="TEntity">实体</typeparam>
    public class EfRepositoryBase<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextProvider">数据上下文提供者</param>
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}