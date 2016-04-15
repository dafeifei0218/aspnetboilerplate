using Abp.Domain.Entities;

namespace Abp.NHibernate.EntityMappings
{
    /// <summary>
    /// A shortcut of <see cref="EntityMap{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 一个快捷的实体映射，使用(<see cref="int"/>)作为主键。
    /// </summary>
    /// <typeparam name="TEntity">Entity map 实体</typeparam>
    public abstract class EntityMap<TEntity> : EntityMap<TEntity, int> where TEntity : IEntity<int>
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="tableName">Table name 表名</param>
        protected EntityMap(string tableName)
            : base(tableName)
        {

        }
    }
}