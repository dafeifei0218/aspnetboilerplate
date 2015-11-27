namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IInputDto"/> can be directly used (or inherited)
    /// to pass an nullable Id value to an application service method.
    /// 空Id的输入数据传输对象
    /// </summary>
    /// <typeparam name="TId">Type of the Id</typeparam>
    public class NullableIdInput<TId> : IInputDto
        where TId : struct
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TId? Id { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NullableIdInput()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">主键</param>
        public NullableIdInput(TId? id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// A shortcut of <see cref="NullableIdInput{TPrimaryKey}"/> for <see cref="int"/>.
    /// 空Id的输入数据传输对象，主键为int
    /// </summary>
    public class NullableIdInput : NullableIdInput<int>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NullableIdInput()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">主键</param>
        public NullableIdInput(int? id)
            : base(id)
        {

        }
    }
}