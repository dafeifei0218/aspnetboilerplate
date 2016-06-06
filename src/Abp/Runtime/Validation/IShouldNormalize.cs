namespace Abp.Runtime.Validation
{
    /// <summary>
    /// This interface is used to normalize inputs before method execution.
    /// 正常化接口，此接口用于在方法执行前规范输入
    /// </summary>
    /// <remarks>
    /// 该接口定义了Normalize方法，实现该方法可以在Validation 后，使用前，对DTO做最后的处理。
    /// </remarks>
    public interface IShouldNormalize
    {
        /// <summary>
        /// This method is called lastly before method execution (after validation if exists).
        /// 规范方法，
        /// 这个方法在最后的方法前执行（验证后，如果存在）。
        /// </summary>
        void Normalize();
    }
}