using System;
using Abp.Domain.Entities;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to pass data for an event that is related to with an <see cref="IEntity"/> object.
    /// 实体事件数据类
    /// </summary>
    /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
    [Serializable]
    public class EntityEventData<TEntity> : EventData , IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// Related entity with this event.
        /// 实体，与此事件相关的实体
        /// </summary>
        public TEntity Entity { get; private set; }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="entity">Related entity with this event 实体，与此事件相关的实体</param>
        public EntityEventData(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// 获取构造函数参数
        /// </summary>
        /// <returns></returns>
        public virtual object[] GetConstructorArgs()
        {
            return new object[] { Entity };
        }
    }
}