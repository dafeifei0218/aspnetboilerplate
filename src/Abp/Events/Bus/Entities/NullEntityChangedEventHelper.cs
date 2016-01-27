namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Null-object implementation of <see cref="IEntityChangedEventHelper"/>.
    /// 空实体更改事件帮助类
    /// </summary>
    public class NullEntityChangedEventHelper : IEntityChangedEventHelper
    {
        /// <summary>
        /// Gets single instance of <see cref="NullEventBus"/> class.
        /// 单例实例
        /// </summary>
        public static NullEntityChangedEventHelper Instance { get { return SingletonInstance; } }
        private static readonly NullEntityChangedEventHelper SingletonInstance = new NullEntityChangedEventHelper();

        /// <summary>
        /// 构造函数
        /// </summary>
        private NullEntityChangedEventHelper()
        {

        }

        /// <summary>
        /// 触发实体创建事件
        /// </summary>
        /// <param name="entity">实体</param>
        public void TriggerEntityCreatedEvent(object entity)
        {
            
        }

        /// <summary>
        /// 触发实体更新事件
        /// </summary>
        /// <param name="entity">实体</param>
        public void TriggerEntityUpdatedEvent(object entity)
        {
            
        }

        /// <summary>
        /// 触发实体删除事件
        /// </summary>
        /// <param name="entity">实体</param>
        public void TriggerEntityDeletedEvent(object entity)
        {
            
        }
    }
}