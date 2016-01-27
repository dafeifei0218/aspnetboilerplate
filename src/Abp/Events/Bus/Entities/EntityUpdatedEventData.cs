using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify update of an Entity.
    /// ʵ������¼�������
    /// </summary>
    /// <typeparam name="TEntity">Entity type ʵ������</typeparam>
    [Serializable]
    public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is updated ʵ��</param>
        public EntityUpdatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}