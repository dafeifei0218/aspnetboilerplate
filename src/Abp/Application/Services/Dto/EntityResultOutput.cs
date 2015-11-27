using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IOutputDto"/> can be used to send Id of an entity as response from an <see cref="IApplicationService"/> method.
    /// ʵ�������ģ��
    /// </summary>
    [Serializable]
    public class EntityResultOutput : EntityResultOutput<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityResultOutput"/> object.
        /// ���캯��
        /// </summary>
        public EntityResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityResultOutput"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="id">Id of the entity ʵ�������</param>
        public EntityResultOutput(int id)
            : base(id)
        {

        }
    }
}