using System;
using Abp.Domain.Entities.Auditing;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IFullAudited{TUser}"/> interface.
    /// 全部审计实体数据传输对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key 主键类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// Is this entity deleted?
        /// 是否删除实体
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Deleter user's Id, if this entity is deleted,
        /// 删除实体的用户Id
        /// </summary>
        public long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time, if this entity is deleted,
        /// 删除实体的时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}