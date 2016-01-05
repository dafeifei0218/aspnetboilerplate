using System.Reflection;

namespace Abp.Dependency
{
    /// <summary>
    /// Used to pass needed objects on conventional registration process.
    /// 常规注册上下文接口，
    /// </summary>
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        /// Gets the registering Assembly.
        /// 获取注册程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Reference to the IOC Container to register types.
        /// IOC控制反转管理类，引用ICO容器注册类型
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// Registration configuration.
        /// 常规注册配置
        /// </summary>
        ConventionalRegistrationConfig Config { get; }
    }
}