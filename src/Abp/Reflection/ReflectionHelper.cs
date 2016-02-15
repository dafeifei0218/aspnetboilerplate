using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Abp.Reflection
{
    /// <summary>
    /// Defines helper methods for reflection.
    /// 反射帮助类
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// Checks whether <paramref name="givenType"/> implements/inherits <paramref name="genericType"/>.
        /// 是否可以转换为泛型类型，
        /// 检查给定的类型是否实现/继承泛型类型
        /// </summary>
        /// <param name="givenType">Type to check 类型检查</param>
        /// <param name="genericType">Generic type 泛型类型</param>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            //给定的类型是泛型类型，并且其可用于构造当前泛型类型的泛型定义为genericType（泛型类型）
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            //如果给定类型继承的类型为nul，返回false
            if (givenType.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }

        /// <summary>
        /// Gets a list of attributes defined for a class member and it's declaring type including inherited attributes.
        /// 获取成员属性和声明类型，获取为类成员定义的属性列表，它声明类型包括继承的类型 
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute 属性类型</typeparam>
        /// <param name="memberInfo">MemberInfo 成员属性信息</param>
        public static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo) 
            where TAttribute : Attribute
        {
            var attributeList = new List<TAttribute>();

            //Add attributes on the member
            //添加属性的成员
            if (memberInfo.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            //Add attributes on the class
            //在类中添加属性
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            return attributeList;
        }
    }
}