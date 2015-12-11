using System;
using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// ��̬Ȩ�޼��
    /// </summary>
    internal static class StaticPermissionChecker
    {
        //ʵ��
        public static IPermissionChecker Instance { get { return LazyInstance.Value; } }
        //�ӳ�ʵ��
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