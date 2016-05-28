using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Defines a cache that can be store and get items by keys.
    /// 缓存接口，
    /// 定义一个缓存，可以通过安检存储和获取项目。
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// Unique name of the cache.
        /// 缓存唯一名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Default sliding expire time of cache items.
        /// Default value: 60 minutes. Can be changed by configuration.
        /// 默认缓存过期时间
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Gets an item from the cache.
        /// 获取缓存的项
        /// </summary>
        /// <param name="key">Key 键，缓存中一个条目的键（字符串类型）</param>
        /// <param name="factory">Factory method to create cache item if not exists 工厂方法创建缓存项，如果不存在</param>
        /// <returns>Cached item 缓存的项</returns>
        object Get(string key, Func<string, object> factory);

        /// <summary>
        /// Gets an item from the cache.
        /// 获取缓存的项
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <param name="factory">Factory method to create cache item if not exists 工厂方法</param>
        /// <returns>Cached item 缓存的项</returns>
        Task<object> GetAsync(string key, Func<string, Task<object>> factory);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// 获取缓存的项，如果未找到则返回null
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <returns>Cached item or null if not found 缓存的项，如果未找到则返回null</returns>
        object GetOrDefault(string key);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// 获取缓存的项，如果未找到则返回null
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <returns>Cached item or null if not found 缓存的项，如果未找到则返回null</returns>
        Task<object> GetOrDefaultAsync(string key);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// 根据键保存/重写缓存的项
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <param name="value">Value 值</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        void Set(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// 根据键保存/重写缓存的项
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <param name="value">Value 值</param>
        /// <param name="slidingExpireTime">Sliding expire time </param>
        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Removes a cache item by it's key.
        /// 删除缓存
        /// </summary>
        /// <param name="key">Key 键</param>
        void Remove(string key);

        /// <summary>
        /// Removes a cache item by it's key (does nothing if given key does not exists in the cache).
        /// 删除缓存-异步
        /// </summary>
        /// <param name="key">Key 键</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// Clears all items in this cache.
        /// 清除全部缓存项
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all items in this cache.
        /// 清除全部缓存项-异步
        /// </summary>
        Task ClearAsync();
    }
}