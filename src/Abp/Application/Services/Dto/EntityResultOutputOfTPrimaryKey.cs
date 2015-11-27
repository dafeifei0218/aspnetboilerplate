using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IOutputDto"/> can be used to send Id of an entity as response from an <see cref="IApplicationService"/> method.
    /// 实体结果输出模型
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of entity 实体的主键类型</typeparam>
    [Serializable]
    public class EntityResultOutput<TPrimaryKey> : EntityDto<TPrimaryKey>, IOutputDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityResultOutput{TPrimaryKey}"/> object.
        /// 构造函数
        /// </summary>
        public EntityResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityResultOutput{TPrimaryKey}"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="id">Id of the entity 实体主键</param>
        public EntityResultOutput(TPrimaryKey id)
            : base(id)
        {

        }
    }
}