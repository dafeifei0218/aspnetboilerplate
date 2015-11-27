using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Implements common properties for entity based DTOs.
    /// 实体数据传输对象，实现了基于实体的DTO的共同性质。
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key 主键类型</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// Id of the entity.
        /// 实体的主键
        /// </summary>
        public TPrimaryKey Id { get; set; }

        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// 构造函数
        /// </summary>
        public EntityDto()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="id">Id of the entity 实体的主键</param>
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }
}
