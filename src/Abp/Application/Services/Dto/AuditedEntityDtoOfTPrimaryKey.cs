using System;
using Abp.Domain.Entities.Auditing;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IAudited{TUser}"/> interface.
    /// 审计实体数据传输对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key 主键类型</typeparam>
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// 最后修改实体的时间。
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// 最后修改实体的该用户。
        /// </summary>
        public long? LastModifierUserId { get; set; }
    }
}