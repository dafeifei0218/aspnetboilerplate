using System;
using System.Reflection;
using Abp.Application.Services;
using Abp.Domain.Repositories;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// A helper class to simplify unit of work process.
    /// 工作单元帮助类
    /// </summary>
    internal static class UnitOfWorkHelper
    {
        /// <summary>
        /// Returns true if UOW must be used for given type as convention.
        /// 是否是传统的工作单元类
        /// </summary>
        /// <param name="type">Type to check 检查类型</param>
        public static bool IsConventionalUowClass(Type type)
        {
            //如果给定的类型，是从IRepository或IApplicationService类分配，返回true
            return typeof(IRepository).IsAssignableFrom(type) || typeof(IApplicationService).IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns true if given method has UnitOfWorkAttribute attribute.
        /// 包含工作单元属性，如果给定的方法有UnitOfWorkAttribute属性，返回true
        /// </summary>
        /// <param name="methodInfo">Method info to check 检查方法信息</param>
        public static bool HasUnitOfWorkAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        /// <summary>
        /// Returns UnitOfWorkAttribute it exists.
        /// 获取UnitOfWorkAttribute属性或空
        /// </summary>
        /// <param name="methodInfo">Method info to check 检查方法信息</param>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof (UnitOfWorkAttribute), false);
            if (attrs.Length <= 0)
            {
                return null;
            }

            return (UnitOfWorkAttribute) attrs[0];
        }
    }
}