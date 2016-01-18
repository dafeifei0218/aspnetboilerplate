using System;
using System.Reflection;
using Abp.Application.Services;
using Abp.Domain.Repositories;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// A helper class to simplify unit of work process.
    /// ������Ԫ������
    /// </summary>
    internal static class UnitOfWorkHelper
    {
        /// <summary>
        /// Returns true if UOW must be used for given type as convention.
        /// �Ƿ��Ǵ�ͳ�Ĺ�����Ԫ��
        /// </summary>
        /// <param name="type">Type to check �������</param>
        public static bool IsConventionalUowClass(Type type)
        {
            //������������ͣ��Ǵ�IRepository��IApplicationService����䣬����true
            return typeof(IRepository).IsAssignableFrom(type) || typeof(IApplicationService).IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns true if given method has UnitOfWorkAttribute attribute.
        /// ����������Ԫ���ԣ���������ķ�����UnitOfWorkAttribute���ԣ�����true
        /// </summary>
        /// <param name="methodInfo">Method info to check ��鷽����Ϣ</param>
        public static bool HasUnitOfWorkAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        /// <summary>
        /// Returns UnitOfWorkAttribute it exists.
        /// ��ȡUnitOfWorkAttribute���Ի��
        /// </summary>
        /// <param name="methodInfo">Method info to check ��鷽����Ϣ</param>
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