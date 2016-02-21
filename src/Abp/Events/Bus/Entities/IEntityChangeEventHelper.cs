namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// ʵ������¼������ӿ�
    /// </summary>
    public interface IEntityChangeEventHelper
    {
        /// <summary>
        /// ����ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityCreatingEvent(object entity);

        /// <summary>
        /// �ڹ�����Ԫ��ɺ󣬴���ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityCreatedEventOnUowCompleted(object entity);

        /// <summary>
        /// ����ʵ������¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityUpdatingEvent(object entity);
        
        /// <summary>
        /// �ڹ�����Ԫ��ɺ󣬴���ʵ������¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityUpdatedEventOnUowCompleted(object entity);

        /// <summary>
        /// ����ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityDeletingEvent(object entity);
        
        /// <summary>
        /// �ڹ�����Ԫ��ɺ󣬴���ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityDeletedEventOnUowCompleted(object entity);
    }
}