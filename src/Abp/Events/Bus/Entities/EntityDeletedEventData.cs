using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify deletion of an Entity.
    /// 实体删除事件数据类
    /// </summary>
    /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="entity">The entity which is deleted 实体，被删除的实体</param>
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}