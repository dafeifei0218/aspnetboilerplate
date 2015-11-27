namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IInputDto"/> can be directly used (or inherited)
    /// to pass an nullable Id value to an application service method.
    /// ��Id���������ݴ������
    /// </summary>
    /// <typeparam name="TId">Type of the Id</typeparam>
    public class NullableIdInput<TId> : IInputDto
        where TId : struct
    {
        /// <summary>
        /// ����
        /// </summary>
        public TId? Id { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public NullableIdInput()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id">����</param>
        public NullableIdInput(TId? id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// A shortcut of <see cref="NullableIdInput{TPrimaryKey}"/> for <see cref="int"/>.
    /// ��Id���������ݴ����������Ϊint
    /// </summary>
    public class NullableIdInput : NullableIdInput<int>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public NullableIdInput()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id">����</param>
        public NullableIdInput(int? id)
            : base(id)
        {

        }
    }
}