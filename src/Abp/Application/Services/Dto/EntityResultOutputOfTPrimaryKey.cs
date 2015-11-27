using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IOutputDto"/> can be used to send Id of an entity as response from an <see cref="IApplicationService"/> method.
    /// ʵ�������ģ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of entity ʵ�����������</typeparam>
    [Serializable]
    public class EntityResultOutput<TPrimaryKey> : EntityDto<TPrimaryKey>, IOutputDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityResultOutput{TPrimaryKey}"/> object.
        /// ���캯��
        /// </summary>
        public EntityResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityResultOutput{TPrimaryKey}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="id">Id of the entity ʵ������</param>
        public EntityResultOutput(TPrimaryKey id)
            : base(id)
        {

        }
    }
}