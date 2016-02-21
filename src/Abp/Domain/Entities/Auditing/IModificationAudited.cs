using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified lastly).
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// 修改审计接口。
    /// </summary>
    public interface IModificationAudited : IHasModificationTime
    {
        ///// <summary>
        ///// The last time of modification.
        ///// 最后修改实体的时间。
        ///// </summary>
        //DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user for this entity.
        /// 最后修改实体的该用户。
        /// </summary>
        long? LastModifierUserId { get; set; }
    }
}