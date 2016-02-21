using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
    /// 删除审计接口
    /// </summary>
    //public interface IDeletionAudited : ISoftDelete
    public interface IDeletionAudited : IHasDeletionTime 
    {
        /// <summary>
        /// Which user deleted this entity?
        /// 实体删除的用户Id
        /// </summary>
        long? DeleterUserId { get; set; }

        ///// <summary>
        ///// Deletion time of this entity.
        ///// 实体删除的时间
        ///// </summary>
        //DateTime? DeletionTime { get; set; }
    }
}