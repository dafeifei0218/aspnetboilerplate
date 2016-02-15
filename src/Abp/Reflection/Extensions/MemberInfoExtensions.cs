using System;
using System.Reflection;

namespace Abp.Reflection.Extensions
{
    /// <summary>
    /// Extensions to <see cref="MemberInfo"/>.
    /// 成员属性的信息扩展类
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets a single attribute for a member.
        /// 获取成员的单一属性
        /// </summary>
        /// <typeparam name="T">Type of the attribute 类型的属性</typeparam>
        /// <param name="memberInfo">The member that will be checked for the attribute 将检查属性的成员</param>
        /// <param name="inherit">Include inherited attributes 包括继承属性</param>
        /// <returns>Returns the attribute object if found. Returns null if not found. 如果找到返回属性对象，如果未找到返回null</returns>
        public static T GetSingleAttributeOrNull<T>(this MemberInfo memberInfo, bool inherit = true) where T : class
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0)
            {
                return (T)attrs[0];
            }

            return default(T);
        }
    }
}
