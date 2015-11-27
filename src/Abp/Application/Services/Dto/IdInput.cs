namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This <see cref="IInputDto"/> can be directly used (or inherited)
    /// to pass an Id value to an application service method.
    /// 带主键的输入传输对象
    /// </summary>
    /// <typeparam name="TId">Type of the Id 主键类型</typeparam>
    public class IdInput<TId> : IInputDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IdInput()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">主键</param>
        public IdInput(TId id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// A shortcut of <see cref="IdInput{TPrimaryKey}"/> for <see cref="int"/>.
    /// 带主键的输入传输对象，主键为int
    /// </summary>
    public class IdInput : IdInput<int>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IdInput()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">主键</param>
        public IdInput(int id)
            : base(id)
        {

        }
    }
}