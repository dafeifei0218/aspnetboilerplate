namespace Abp.Domain.Entities
{
    /// <summary>
    /// This interface is used to make an entity active/passive.
    /// 激活接口
    /// </summary>
    public interface IPassivable
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// 是否激活，true：激活，false：未激活
        /// </summary>
        bool IsActive { get; set; }
    }
}