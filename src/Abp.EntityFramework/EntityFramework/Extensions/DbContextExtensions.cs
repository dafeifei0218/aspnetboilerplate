using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Abp.Reflection;

namespace Abp.EntityFramework.Extensions
{
    /// <summary>
    /// 数据上下文扩展类
    /// </summary>
    internal static class DbContextExtensions
    {
        /// <summary>
        /// 获取实体类型集合
        /// </summary>
        /// <param name="dbContextType">数据上下文类型</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetEntityTypes(this Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))
                select property.PropertyType.GenericTypeArguments[0];
        }
    }
}