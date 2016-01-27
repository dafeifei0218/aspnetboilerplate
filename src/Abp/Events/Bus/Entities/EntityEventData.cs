using System;
using Abp.Domain.Entities;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to pass data for an event that is related to with an <see cref="IEntity"/> object.
    /// ʵ���¼�������
    /// </summary>
    /// <typeparam name="TEntity">Entity type ʵ������</typeparam>
    [Serializable]
    public class EntityEventData<TEntity> : EventData , IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// Related entity with this event.
        /// ʵ�壬����¼���ص�ʵ��
        /// </summary>
        public TEntity Entity { get; private set; }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">Related entity with this event ʵ�壬����¼���ص�ʵ��</param>
        public EntityEventData(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// ��ȡ���캯������
        /// </summary>
        /// <returns></returns>
        public virtual object[] GetConstructorArgs()
        {
            return new object[] { Entity };
        }
    }
}