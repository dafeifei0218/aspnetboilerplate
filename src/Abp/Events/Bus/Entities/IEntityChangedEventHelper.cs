namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// ʵ������¼������ӿ�
    /// </summary>
    public interface IEntityChangedEventHelper
    {
        /// <summary>
        /// ����ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityCreatedEvent(object entity);
        
        /// <summary>
        /// ����ʵ������¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityUpdatedEvent(object entity);

        /// <summary>
        /// ����ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityDeletedEvent(object entity);
    }
}