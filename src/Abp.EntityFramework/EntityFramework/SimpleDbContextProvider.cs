using System.Data.Entity;

namespace Abp.EntityFramework
{
    /// <summary>
    /// 简单数据上下文提供者
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public sealed class SimpleDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        public TDbContext DbContext { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public SimpleDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}