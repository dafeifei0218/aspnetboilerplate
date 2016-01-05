using Abp.Configuration;
using Castle.DynamicProxy;

namespace Abp.Dependency
{
    /// <summary>
    /// This class is used to pass configuration/options while registering classes in conventional way.
    /// 常规注册配置，
    /// 这个类使用通过配置/选项，而注册类在常规的方式。
    /// </summary>
    public class ConventionalRegistrationConfig : DictionayBasedConfig
    {
        /// <summary>
        /// Install all <see cref="IInterceptor"/> implementations automatically or not.
        /// Default: true. 
        /// 安装所有的拦截自动或不。默认：true
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// Creates a new <see cref="ConventionalRegistrationConfig"/> object.
        /// 创建一个ConventionalRegistrationConfig对象
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}