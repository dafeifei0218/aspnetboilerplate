using System.Reflection;

namespace Abp.Dependency
{
    /// <summary>
    /// This class is used to pass needed objects on conventional registration process.
    /// 常规注册上下文
    /// </summary>
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        /// <summary>
        /// Gets the registering Assembly.
        /// 获取注册程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Reference to the IOC Container to register types.
        /// IOC控制反转管理类，引用ICO容器注册类型
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Registration configuration.
        /// 常规注册配置
        /// </summary>
        public ConventionalRegistrationConfig Config { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="iocManager">IOC管理</param>
        /// <param name="config">常规注册配置</param>
        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}