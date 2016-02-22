using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ITypedCache{TKey,TValue}"/>.
    /// ���ͻ�����չ��
    /// </summary>
    public static class TypedCacheExtensions
    {
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <typeparam name="TKey">��</typeparam>
        /// <typeparam name="TValue">ֵ</typeparam>
        /// <param name="cache">����</param>
        /// <param name="key">��</param>
        /// <param name="factory"> ��������������������������</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this ITypedCache<TKey, TValue> cache, TKey key, Func<TValue> factory)
        {
            return cache.Get(key, k => factory());
        }

        /// <summary>
        /// ��ȡ����-�첽
        /// </summary>
        /// <typeparam name="TKey">��</typeparam>
        /// <typeparam name="TValue">ֵ</typeparam>
        /// <param name="cache">����</param>
        /// <param name="key">��</param>
        /// <param name="factory"> ��������������������������</param>
        /// <returns></returns>
        public static Task<TValue> GetAsync<TKey, TValue>(this ITypedCache<TKey, TValue> cache, TKey key, Func<Task<TValue>> factory)
        {
            return cache.GetAsync(key, k => factory());
        }
    }
}