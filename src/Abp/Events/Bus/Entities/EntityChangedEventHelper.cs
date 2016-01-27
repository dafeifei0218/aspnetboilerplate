using System;
using Abp.Dependency;
using Abp.Domain.Uow;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// 实体更改事件帮助类，用于触发实体更改事件。
    /// </summary>
    public class EntityChangedEventHelper : ITransientDependency, IEntityChangedEventHelper
    {
        /// <summary>
        /// 事件总线
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 工作单元管理类
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWorkManager">工作单元管理类</param>
        public EntityChangedEventHelper(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// 触发实体创建事件
        /// </summary>
        /// <param name="entity">实体</param>
        public void TriggerEntityCreatedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityCreatedEventData<>), entity);
        }

        /// <summary>
        /// 触发实体更新事件
        /// </summary>
        /// <param name="entity">实体</param>
        public void TriggerEntityUpdatedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityUpdatedEventData<>), entity);
        }

        /// <summary>
        /// 触发实体删除事件
        /// </summary>
        /// <param name="entity">实体</param>
        public void TriggerEntityDeletedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityDeletedEventData<>), entity);
        }

        /// <summary>
        /// 触发实体更改事件
        /// </summary>
        /// <param name="genericEventType">通用事件类型</param>
        /// <param name="entity">实体</param>
        private void TriggerEntityChangeEvent(Type genericEventType, object entity)
        {
            var entityType = entity.GetType();
            var eventType = genericEventType.MakeGenericType(entityType);

            if (_unitOfWorkManager == null || _unitOfWorkManager.Current == null)
            {
                EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, new[] { entity }));
                return;
            }

            _unitOfWorkManager.Current.Completed += (sender, args) => EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, new[] { entity }));
        }
    }
}