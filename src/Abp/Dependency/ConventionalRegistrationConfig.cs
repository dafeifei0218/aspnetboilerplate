using Abp.Configuration;
using Castle.DynamicProxy;

namespace Abp.Dependency
{
    /// <summary>
    /// This class is used to pass configuration/options while registering classes in conventional way.
    /// ����ע�����ã�
    /// �����ʹ��ͨ������/ѡ���ע�����ڳ���ķ�ʽ��
    /// </summary>
    public class ConventionalRegistrationConfig : DictionayBasedConfig
    {
        /// <summary>
        /// Install all <see cref="IInterceptor"/> implementations automatically or not.
        /// Default: true. 
        /// ��װ���е������Զ��򲻡�Ĭ�ϣ�true
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// Creates a new <see cref="ConventionalRegistrationConfig"/> object.
        /// ����һ��ConventionalRegistrationConfig����
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}