namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ICacheManager"/>.
    /// 缓存管理扩展类
    /// </summary>
    public static class CacheManagerExtensions
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cacheManager">缓存管理类</param>
        /// <param name="name">缓存名称</param>
        /// <returns></returns>
        public static ITypedCache<TKey, TValue> GetCache<TKey, TValue>(this ICacheManager cacheManager, string name)
        {
            return cacheManager.GetCache(name).AsTyped<TKey, TValue>();
        }
    }
}