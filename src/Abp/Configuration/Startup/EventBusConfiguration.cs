namespace Abp.Configuration.Startup
{
    /// <summary>
    /// 时间总线配置
    /// </summary>
    internal class EventBusConfiguration : IEventBusConfiguration
    {
        /// <summary>
        /// 用户默认时间总线
        /// </summary>
        public bool UseDefaultEventBus { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EventBusConfiguration()
        {
            UseDefaultEventBus = true;
        }
    }
}