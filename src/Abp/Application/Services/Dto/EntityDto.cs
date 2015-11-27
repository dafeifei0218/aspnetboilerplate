using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="EntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 实体数据传输对象，主键为int
    /// </summary>
    [Serializable]
    public class EntityDto : EntityDto<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// 构造函数
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="id">Id of the entity 主键</param>
        public EntityDto(int id)
            : base(id)
        {
        }
    }
}