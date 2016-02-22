using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// An interface to work with cache in a typed manner.
    /// Use <see cref="CacheExtensions.AsTyped{TKey,TValue}"/> method
    /// to convert a <see cref="ICache"/> to this interface.
    /// 类型缓存接口
    /// </summary>
    /// <typeparam name="TKey">Key type for cache items 缓存项目键类型</typeparam>
    /// <typeparam name="TValue">Value type for cache items</typeparam>
    public interface ITypedCache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Unique name of the cache.
        /// 缓存唯一名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Default sliding expire time of cache items.
        /// 默认过期时间
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Gets the internal cache.
        /// 获取内部缓存
        /// </summary>
        ICache InternalCache { get; }

        /// <summary>
        /// Gets an item from the cache.
        /// 获取缓存值
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <param name="factory">Factory method to create cache item if not exists 工厂方法创建缓存项，如果不存在</param>
        /// <returns>Cached item 缓存值</returns>
        TValue Get(TKey key, Func<TKey, TValue> factory);

        /// <summary>
        /// Gets an item from the cache.
        /// 获取缓存值
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <param name="factory">Factory method to create cache item if not exists 工厂方法创建缓存项，如果不存在</param>
        /// <returns>Cached item 缓存值</returns>
        Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// 获取缓存的项，如果未找到则返回null
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <returns>Cached item or null if not found 缓存的项，如果未找到则返回null</returns>
        TValue GetOrDefault(TKey key);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// 获取缓存的项，如果未找到则返回null-异步
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <returns>Cached item or null if not found 缓存的项，如果未找到则返回null</returns>
        Task<TValue> GetOrDefaultAsync(TKey key);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// 根据键保存/重写缓存的项
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <param name="value">Value 值</param>
        /// <param name="slidingExpireTime">Sliding expire time 过期时间</param>
        void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// 根据键保存/重写缓存的项-异步
        /// </summary>
        /// <param name="key">Key 键</param>
        /// <param name="value">Value 值</param>
        /// <param name="slidingExpireTime">Sliding expire time 过期时间</param>
        Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Removes a cache item by it's key (does nothing if given key does not exists in the cache).
        /// 根据键删除缓存
        /// </summary>
        /// <param name="key">Key 键</param>
        void Remove(TKey key);

        /// <summary>
        /// Removes a cache item by it's key.
        /// 根据键删除缓存
        /// </summary>
        /// <param name="key">Key 键</param>
        Task RemoveAsync(TKey key);

        /// <summary>
        /// Clears all items in this cache.
        /// 清除缓存所有项目
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all items in this cache.
        /// 清除缓存所有项目-异步
        /// </summary>
        Task ClearAsync();
    }
}