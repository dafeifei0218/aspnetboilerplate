using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify update of an Entity.
    /// 实体更新事件数据类
    /// </summary>
    /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
    [Serializable]
    public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="entity">The entity which is updated 实体</param>
        public EntityUpdatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}