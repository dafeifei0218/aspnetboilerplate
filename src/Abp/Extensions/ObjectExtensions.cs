using System;
using System.Globalization;
using System.Linq;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for all objects.
    /// Object扩展类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// 用于装箱或拆箱一种对象
        /// </summary>
        /// <typeparam name="T">Type to be casted 类型</typeparam>
        /// <param name="obj">Object to cast 对象</param>
        /// <returns>Casted object 强转对象</returns>
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// Converts given object to a value type using <see cref="Convert.ChangeType(object,System.TypeCode)"/> method.
        /// 转换对象到指定类型，使用Convert.ChangeType方法
        /// </summary>
        /// <param name="obj">Object to be converted 转换对象</param>
        /// <typeparam name="T">Type of the target object 目标对象类型</typeparam>
        /// <returns>Converted object 转换对象</returns>
        public static T To<T>(this object obj)
            where T : struct
        {
            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Check if an item is in a list.
        /// 检查项目是否在列表中
        /// </summary>
        /// <param name="item">Item to check 检查项目</param>
        /// <param name="list">List of items 项目列表</param>
        /// <typeparam name="T">Type of the items 项目类型</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
