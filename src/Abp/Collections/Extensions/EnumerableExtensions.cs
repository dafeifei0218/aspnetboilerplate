using System;
using System.Collections.Generic;
using System.Linq;

namespace Abp.Collections.Extensions
{    
    /// <summary> 
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// IEnumerable扩展方法
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Concatenates the members of a constructed <see cref="IEnumerable{T}"/> collection of type System.String, using the specified separator between each member.
        /// This is a shortcut for string.Join(...)
        /// 将集合的每个成员之间使用指定的分隔符。
        /// </summary>
        /// <param name="source">A collection that contains the strings to concatenate. 一个集合</param>
        /// <param name="separator">The string to use as a separator. separator is included in the returned string only if values has more than one element. 用作分隔符的字符串。只有在返回的字符串中包含有多个元素时，才有分隔符。</param>
        /// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns System.String.Empty.
        /// 由分隔字符串分隔的值的字符串。如果值没有成员，该方法返回System.String.Empty。</returns>
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// This is a shortcut for string.Join(...)
        /// 将集合的每个成员之间使用指定的分隔符。
        /// </summary>
        /// <param name="source">A collection that contains the objects to concatenate. 一个集合</param>
        /// <param name="separator">The string to use as a separator. separator is included in the returned string only if values has more than one element. 用作分隔符的字符串。只有在返回的字符串中包含有多个元素时，才有分隔符。</param>
        /// <typeparam name="T">The type of the members of values. 类型</typeparam>
        /// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns System.String.Empty.
        /// 由分隔字符串分隔的值的字符串。如果值没有成员，该方法返回System.String.Empty。</returns>
        public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        /// 是否过滤数据
        /// </summary>
        /// <param name="source">Enumerable to apply filtering 集合</param>
        /// <param name="condition">A boolean value 布尔条件，true：过滤；false：不过滤</param>
        /// <param name="predicate">Predicate to filter the enumerable 集合过滤</param>
        /// <returns>Filtered or not filtered enumerable based on <see cref="condition"/> 根据condition条件过滤或不过滤集合</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }

        /// <summary>
        /// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        /// 是否过滤数据
        /// </summary>
        /// <param name="source">Enumerable to apply filtering 集合</param>
        /// <param name="condition">A boolean value 布尔条件，true：过滤；false：不过滤</param>
        /// <param name="predicate">Predicate to filter the enumerable 集合过滤</param>
        /// <returns>Filtered or not filtered enumerable based on <see cref="condition"/> 根据condition条件过滤或不过滤集合</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }
    }
}
