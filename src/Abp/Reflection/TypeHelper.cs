using System;
using System.Net.Configuration;

namespace Abp.Reflection
{
    /// <summary>
    /// Some simple type-checking methods used internally.
    /// 类型帮助
    /// </summary>
    internal static class TypeHelper
    {
        /// <summary>
        /// 是否Func有返回值的委托
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>true：是；false：否</returns>
        public static bool IsFunc(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = obj.GetType();
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Func<>);
        }

        /// <summary>
        /// 是否Func有返回值的委托
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsFunc<TReturn>(object obj)
        {
            return obj != null && obj.GetType() == typeof(Func<TReturn>);
        }

        /// <summary>
        /// 是否为基元类型，包括Nullable空
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsPrimitiveExtendedIncludingNullable(Type type)
        {
            if (IsPrimitiveExtended(type))
            {
                return true;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsPrimitiveExtended(type.GenericTypeArguments[0]);
            }

            return false;
        }

        /// <summary>
        /// 是否为基元类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static bool IsPrimitiveExtended(Type type)
        {
            if (type.IsPrimitive)
            {
                return true;
            }

            return type == typeof (string) ||
                   type == typeof (decimal) ||
                   type == typeof (DateTime) ||
                   type == typeof (DateTimeOffset) ||
                   type == typeof (TimeSpan) ||
                   type == typeof (Guid);
        }
    }
}
