using System.Data.Entity;

namespace Abp.EntityFramework
{
    /// <summary>
    /// DbContext数据上下文提供者
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        TDbContext DbContext { get; }
    }
}