using System;
using System.Collections.Generic;

namespace Abp.Collections
{
    /// <summary>
    /// A shortcut for <see cref="ITypeList{TBaseType}"/> to use object as base type.
    /// �����б�ӿ�
    /// </summary>
    public interface ITypeList : ITypeList<object>
    {

    }

    /// <summary>
    /// Extends <see cref="IList{Type}"/> to add restriction a specific base type.
    /// �����б�ӿ�
    /// </summary>
    /// <typeparam name="TBaseType">Base Type of <see cref="Type"/>s in this list �б�Ļ�������</typeparam>
    public interface ITypeList<in TBaseType> : IList<Type>
    {
        /// <summary>
        /// Adds a type to list.
        /// ������͵��б�
        /// </summary>
        /// <typeparam name="T">Type ����</typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// Checks if a type exists in the list.
        /// ����б����Ƿ���ڸ�����
        /// </summary>
        /// <typeparam name="T">Type ����</typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// Removes a type from list
        /// ���б��Ƴ�����
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        void Remove<T>() where T : TBaseType;
    }
}