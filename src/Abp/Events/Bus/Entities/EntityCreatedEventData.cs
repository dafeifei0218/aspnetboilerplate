using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify creation of an Entity.
    /// ʵ�崴���¼�������
    /// </summary>
    /// <typeparam name="TEntity">Entity type ʵ������</typeparam>
    [Serializable]
    public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is created ʵ�壬������ʵ��</param>
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}