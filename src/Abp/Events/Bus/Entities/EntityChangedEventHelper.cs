using System;
using Abp.Dependency;
using Abp.Domain.Uow;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// ʵ������¼������࣬���ڴ���ʵ������¼���
    /// </summary>
    public class EntityChangedEventHelper : ITransientDependency, IEntityChangedEventHelper
    {
        /// <summary>
        /// �¼�����
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// ������Ԫ������
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="unitOfWorkManager">������Ԫ������</param>
        public EntityChangedEventHelper(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// ����ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityCreatedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityCreatedEventData<>), entity);
        }

        /// <summary>
        /// ����ʵ������¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityUpdatedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityUpdatedEventData<>), entity);
        }

        /// <summary>
        /// ����ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityDeletedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityDeletedEventData<>), entity);
        }

        /// <summary>
        /// ����ʵ������¼�
        /// </summary>
        /// <param name="genericEventType">ͨ���¼�����</param>
        /// <param name="entity">ʵ��</param>
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