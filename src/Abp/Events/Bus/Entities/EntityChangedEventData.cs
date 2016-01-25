using System;
using Abp.Domain.Entities;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to pass data for an event that is related to with a changed <see cref="IEntity"/> object.
    /// 实体更改事件数据
    /// </summary>
    /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="entity">Changed entity in this event 实体</param>
        public EntityChangedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}