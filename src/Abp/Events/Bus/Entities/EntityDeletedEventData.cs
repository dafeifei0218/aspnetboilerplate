using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify deletion of an Entity.
    /// ʵ��ɾ���¼�������
    /// </summary>
    /// <typeparam name="TEntity">Entity type ʵ������</typeparam>
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is deleted ʵ�壬��ɾ����ʵ��</param>
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}