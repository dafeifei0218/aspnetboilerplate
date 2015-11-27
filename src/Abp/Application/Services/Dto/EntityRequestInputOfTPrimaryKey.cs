using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IInputDto"/> can be used to send Id of an entity to an <see cref="IApplicationService"/> method.
    /// 实体请求输入模型
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of entity 实体主键</typeparam>
    [Serializable]
    public class EntityRequestInput<TPrimaryKey> : EntityDto<TPrimaryKey>, IInputDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityRequestInput{TPrimaryKey}"/> object.
        /// 构造函数
        /// </summary>
        public EntityRequestInput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityRequestInput{TPrimaryKey}"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="id">Id of the entity 实体的主键</param>
        public EntityRequestInput(TPrimaryKey id)
            : base(id)
        {

        }
    }
}