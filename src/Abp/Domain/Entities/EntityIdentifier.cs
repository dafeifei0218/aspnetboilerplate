using System;

namespace Abp.Domain.Entities
{
    /// <summary>
    /// Used to identify an entity.
    /// Can be used to store an entity <see cref="Type"/> and <see cref="Id"/>.
    /// 实体标识符，
    /// 用于识别一个实体。
    /// 可以存储一个实体<see cref="Type"/>类型和<see cref="Id"/>Id。
    /// </summary>
    [Serializable]
    public class EntityIdentifier
    {
        /// <summary>
        /// Entity Type.
        /// 实体类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Entity's Id.
        /// 实体Id
        /// </summary>
        public object Id { get; private set; }

        /// <summary>
        /// Added for serialization purposes.
        /// 构造函数，增加了序列化的目的。
        /// </summary>
        private EntityIdentifier()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityIdentifier"/> class.
        /// 初始化一个新的<see cref="EntityIdentifier"/>实体标识符类
        /// </summary>
        /// <param name="type">Entity type. 实体类型</param>
        /// <param name="id">Id of the entity. 实体Id</param>
        public EntityIdentifier(Type type, object id)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Type = type;
            Id = id;
        }
    }
}