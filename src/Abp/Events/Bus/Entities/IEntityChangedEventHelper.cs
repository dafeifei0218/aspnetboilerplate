namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// 实体更改事件帮助接口
    /// </summary>
    public interface IEntityChangedEventHelper
    {
        /// <summary>
        /// 触发实体创建事件
        /// </summary>
        /// <param name="entity">实体</param>
        void TriggerEntityCreatedEvent(object entity);
        
        /// <summary>
        /// 触发实体更新事件
        /// </summary>
        /// <param name="entity">实体</param>
        void TriggerEntityUpdatedEvent(object entity);

        /// <summary>
        /// 触发实体删除事件
        /// </summary>
        /// <param name="entity">实体</param>
        void TriggerEntityDeletedEvent(object entity);
    }
}