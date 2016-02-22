using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ITypedCache{TKey,TValue}"/>.
    /// 类型缓存扩展类
    /// </summary>
    public static class TypedCacheExtensions
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory"> 工厂方法创建缓存项，如果不存在</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this ITypedCache<TKey, TValue> cache, TKey key, Func<TValue> factory)
        {
            return cache.Get(key, k => factory());
        }

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory"> 工厂方法创建缓存项，如果不存在</param>
        /// <returns></returns>
        public static Task<TValue> GetAsync<TKey, TValue>(this ITypedCache<TKey, TValue> cache, TKey key, Func<Task<TValue>> factory)
        {
            return cache.GetAsync(key, k => factory());
        }
    }
}