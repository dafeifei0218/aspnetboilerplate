using System;
using System.Collections.Generic;

namespace Abp.Collections.Extensions
{
    /// <summary>
    /// Extension methods for Collections.
    /// ���ͼ�����չ������
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// ����κθ����ļ��϶����Ƿ�Ϊnull��û����Ŀ
        /// </summary>
        /// <param name="source">Collection ����</param>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// ������ڼ����У��򽫸�����ӵ������С�
        /// </summary>
        /// <param name="source">Collection ����</param>
        /// <param name="item">Item to check and add ��Ŀ</param>
        /// <typeparam name="T">Type of the items in the collection ���͵Ķ���ļ���</typeparam>
        /// <returns>Returns True if added, returns False if not. true����ӣ�false��δ���</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            //��������а���item���򷵻�false
            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}