using System;
using System.Collections.Generic;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// An upper level container for <see cref="ICache"/> objects. 
    /// A cache manager should work as Singleton and track and manage <see cref="ICache"/> objects.
    /// 缓存管理接口
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Gets all caches.
        /// 获取全部缓存
        /// </summary>
        /// <returns>List of caches 缓存列表</returns>
        IReadOnlyList<ICache> GetAllCaches();

        /// <summary>
        /// Gets (or creates) a cache.
        /// 获取（或创建）缓存
        /// </summary>
        /// <param name="name">Unique name of the cache 缓存唯一名称</param>
        /// <returns>The cache reference 缓存</returns>
        ICache GetCache(string name);
    }
}
