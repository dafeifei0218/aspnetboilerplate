namespace Abp.Domain.Entities
{
    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// 实体接口，泛型为实体主键类型
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity 实体主键类型</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// 主键
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an <see cref="Id"/>).
        /// 检查实体是否是瞬态的（没有Id不持久化到数据库）
        /// </summary>
        /// <returns>True, if this entity is transient. true：表示实体是瞬态的</returns>
        bool IsTransient();
    }
}
