using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IOutputDto"/> can be used to send Id of an entity as response from an <see cref="IApplicationService"/> method.
    /// 实体结果输出模型
    /// </summary>
    [Serializable]
    public class EntityResultOutput : EntityResultOutput<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityResultOutput"/> object.
        /// 构造函数
        /// </summary>
        public EntityResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityResultOutput"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="id">Id of the entity 实体的主键</param>
        public EntityResultOutput(int id)
            : base(id)
        {

        }
    }
}