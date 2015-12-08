using System;
using System.Collections.Generic;

namespace Abp.Collections
{
    /// <summary>
    /// A shortcut for <see cref="ITypeList{TBaseType}"/> to use object as base type.
    /// 类型列表接口
    /// </summary>
    public interface ITypeList : ITypeList<object>
    {

    }

    /// <summary>
    /// Extends <see cref="IList{Type}"/> to add restriction a specific base type.
    /// 类型列表接口
    /// </summary>
    /// <typeparam name="TBaseType">Base Type of <see cref="Type"/>s in this list 列表的基础类型</typeparam>
    public interface ITypeList<in TBaseType> : IList<Type>
    {
        /// <summary>
        /// Adds a type to list.
        /// 添加类型到列表
        /// </summary>
        /// <typeparam name="T">Type 类型</typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// Checks if a type exists in the list.
        /// 检查列表中是否存在该类型
        /// </summary>
        /// <typeparam name="T">Type 类型</typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// Removes a type from list
        /// 从列表移除类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        void Remove<T>() where T : TBaseType;
    }
}