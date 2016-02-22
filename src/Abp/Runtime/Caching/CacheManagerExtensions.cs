namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ICacheManager"/>.
    /// ���������չ��
    /// </summary>
    public static class CacheManagerExtensions
    {
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <typeparam name="TKey">���͵ļ�</typeparam>
        /// <typeparam name="TValue">���͵�ֵ</typeparam>
        /// <param name="cacheManager">���������</param>
        /// <param name="name">��������</param>
        /// <returns></returns>
        public static ITypedCache<TKey, TValue> GetCache<TKey, TValue>(this ICacheManager cacheManager, string name)
        {
            return cacheManager.GetCache(name).AsTyped<TKey, TValue>();
        }
    }
}