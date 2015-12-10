using Abp.Application.Services;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Authorization.Interceptors
{
    /// <summary>
    /// This class is used to register interceptors on the Application Layer.
    /// ע����Ȩ������
    /// </summary>
    internal static class AuthorizationInterceptorRegistrar
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="iocManager">IOC���Ʒ�ת������</param>
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;            
        }

        /// <summary>
        /// �ں����ע��
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="handler">�������</param>
        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuthorizationInterceptor))); 
            }
        }
    }
}