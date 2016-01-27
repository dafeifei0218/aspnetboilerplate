using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify creation of an Entity.
    /// 实体创建事件数据类
    /// </summary>
    /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
    [Serializable]
    public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="entity">The entity which is created 实体，创建的实体</param>
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}