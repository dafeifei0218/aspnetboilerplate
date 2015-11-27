namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IInputDto"/> can be directly used (or inherited)
    /// to pass an Id value to an application service method.
    /// �����������봫�����
    /// </summary>
    /// <typeparam name="TId">Type of the Id ��������</typeparam>
    public class IdInput<TId> : IInputDto
    {
        /// <summary>
        /// ����
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public IdInput()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id">����</param>
        public IdInput(TId id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// A shortcut of <see cref="IdInput{TPrimaryKey}"/> for <see cref="int"/>.
    /// �����������봫���������Ϊint
    /// </summary>
    public class IdInput : IdInput<int>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public IdInput()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id">����</param>
        public IdInput(int id)
            : base(id)
        {

        }
    }
}