using Abp.Dependency;
using Abp.Events.Bus;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure <see cref="IEventBus"/>.
    /// 事件总线配置接口
    /// </summary>
    public interface IEventBusConfiguration
    {
        /// <summary>
        /// True, to use <see cref="EventBus.Default"/>.
        /// False, to create per <see cref="IIocManager"/>.
        /// This is generally set to true. But, for unit tests,
        /// it can be set to false.
        /// Default: true.
        /// 是否使用默认事件总线，
        /// true：使用；
        /// false：使用IIocManager；
        /// 默认：true
        /// </summary>
        bool UseDefaultEventBus { get; set; }
    }
}