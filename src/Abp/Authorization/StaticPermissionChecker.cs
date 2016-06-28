using System;
using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// ��̬Ȩ�޼��
    /// </summary>
    /// <remarks>
    /// ���ڴ���������IPermissionChecker�ӿڵ�ʵ�֣�
    /// ���û���Զ����IPermissionCheckerʵ�ֱ�ע�뵽�������򷵻�NullPermissionChecker��
    /// ���ͨ��Lazyʵ���ӳټ��ء�
    /// </remarks>
    internal static class StaticPermissionChecker
    {
        /// <summary>
        /// ʵ��
        /// </summary>
        public static IPermissionChecker Instance { get { return LazyInstance.Value; } }
        /// <summary>
        /// �ӳ�ʵ��
        /// </summary>
        private static readonly Lazy<IPermissionChecker> LazyInstance;

        /// <summary>
        /// ���캯��
        /// </summary>
        static StaticPermissionChecker()
        {
            //���IOC�������Ѿ�ע��IPermissionCheckerȨ�޼�飬�����IPermissionChecker������ʹ��Ĭ��Ȩ�޼��
            LazyInstance = new Lazy<IPermissionChecker>(
                () => IocManager.Instance.IsRegistered<IPermissionChecker>()
                    ? IocManager.Instance.Resolve<IPermissionChecker>()
                    : NullPermissionChecker.Instance
                );
        }
    }
}