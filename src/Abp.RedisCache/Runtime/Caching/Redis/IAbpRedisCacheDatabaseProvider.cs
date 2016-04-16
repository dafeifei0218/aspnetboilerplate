using StackExchange.Redis;

namespace Abp.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to get <see cref="IDatabase"/> for Redis cache.
    /// Abp Redis缓存数据提供者接口
    /// </summary>
    public interface IAbpRedisCacheDatabaseProvider 
    {
        /// <summary>
        /// Gets the database connection.
        /// 获取数据连接
        /// </summary>
        IDatabase GetDatabase();
    }
}
