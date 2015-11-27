using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IInputDto"/> can be used to send Id of an entity to an <see cref="IApplicationService"/> method.
    /// ʵ����������ģ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of entity ʵ������</typeparam>
    [Serializable]
    public class EntityRequestInput<TPrimaryKey> : EntityDto<TPrimaryKey>, IInputDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityRequestInput{TPrimaryKey}"/> object.
        /// ���캯��
        /// </summary>
        public EntityRequestInput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityRequestInput{TPrimaryKey}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="id">Id of the entity ʵ�������</param>
        public EntityRequestInput(TPrimaryKey id)
            : base(id)
        {

        }
    }
}