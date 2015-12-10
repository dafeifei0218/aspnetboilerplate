using System;
using System.Collections.Generic;

namespace Abp.Collections.Extensions
{
    /// <summary>
    /// Extension methods for Collections.
    /// 泛型集合扩展方法。
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// 检查任何给定的集合对象是否为null或没有项目
        /// </summary>
        /// <param name="source">Collection 集合</param>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// 如果不在集合中，则将该项添加到集合中。
        /// </summary>
        /// <param name="source">Collection 集合</param>
        /// <param name="item">Item to check and add 项目</param>
        /// <typeparam name="T">Type of the items in the collection 类型的对象的集合</typeparam>
        /// <returns>Returns True if added, returns False if not. true：添加；false：未添加</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            //如果集合中包含item，则返回false
            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}