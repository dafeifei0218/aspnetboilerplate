using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// Ȩ������������
    /// </summary>
    /// <remarks>
    /// ��Խӿں�ʵ�������½�һ��Permission��PermissionDictionary�У��͸���Permission��Name��PermissionDictionary����һ��Permission��
    /// </remarks>
    internal class PermissionDependencyContext : IPermissionDependencyContext, ITransientDependency
    {
        /// <summary>
        /// �û�Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// IOC���Ʒ�ת������
        /// </summary>
        public IIocResolver IocResolver { get; private set; }
        
        /// <summary>
        /// Ȩ�޼��
        /// </summary>
        public IPermissionChecker PermissionChecker { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver">IOC���Ʒ�ת������</param>
        public PermissionDependencyContext(IIocResolver iocResolver)
        {
            IocResolver = iocResolver;
            PermissionChecker = NullPermissionChecker.Instance;
        }
    }
}