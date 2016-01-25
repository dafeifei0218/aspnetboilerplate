using System;
using Abp.Domain.Entities;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to pass data for an event that is related to with a changed <see cref="IEntity"/> object.
    /// ʵ������¼�����
    /// </summary>
    /// <typeparam name="TEntity">Entity type ʵ������</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">Changed entity in this event ʵ��</param>
        public EntityChangedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}