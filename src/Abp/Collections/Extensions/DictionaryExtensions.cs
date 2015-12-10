using System.Collections.Generic;

namespace Abp.Collections.Extensions
{
    /// <summary>
    /// Extension methods for Dictionary.
    /// 表示键/值对的泛型集合扩展方法。
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// This method is used to try to get a value in a dictionary if it does exists.
        /// 获取与指定的键相关联的值。试图获取值，如果他确实存在，尝试在字典中找到一个值。
        /// </summary>
        /// <typeparam name="T">Type of the value 类型值</typeparam>
        /// <param name="dictionary">The collection object 字典对象</param>
        /// <param name="key">Key 键</param>
        /// <param name="value">Value of the key (or default value if key not exists) 键的值（或默认值）</param>
        /// <returns>True if key does exists in the dictionary. ture：存在于字典中</returns>
        internal static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            object valueObj;
            if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
            {
                value = (T)valueObj;
                return true;
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// Gets a value from the dictionary with given key. Returns default value if can not find.
        /// 获取与指定的键相关联的值。
        /// </summary>
        /// <param name="dictionary">Dictionary to check and get 字典对象</param>
        /// <param name="key">Key to find the value 键</param>
        /// <typeparam name="TKey">Type of the key 键的类型</typeparam>
        /// <typeparam name="TValue">Type of the value 值的类型</typeparam>
        /// <returns>Value if found, default if can not found. 如果找到返回值，如果未找到返回default</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default(TValue);
        }
    }
}