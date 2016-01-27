namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Null-object implementation of <see cref="IEntityChangedEventHelper"/>.
    /// ��ʵ������¼�������
    /// </summary>
    public class NullEntityChangedEventHelper : IEntityChangedEventHelper
    {
        /// <summary>
        /// Gets single instance of <see cref="NullEventBus"/> class.
        /// ����ʵ��
        /// </summary>
        public static NullEntityChangedEventHelper Instance { get { return SingletonInstance; } }
        private static readonly NullEntityChangedEventHelper SingletonInstance = new NullEntityChangedEventHelper();

        /// <summary>
        /// ���캯��
        /// </summary>
        private NullEntityChangedEventHelper()
        {

        }

        /// <summary>
        /// ����ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityCreatedEvent(object entity)
        {
            
        }

        /// <summary>
        /// ����ʵ������¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityUpdatedEvent(object entity)
        {
            
        }

        /// <summary>
        /// ����ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityDeletedEvent(object entity)
        {
            
        }
    }
}