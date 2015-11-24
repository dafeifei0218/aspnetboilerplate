using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// An entity can implement this interface if <see cref="CreationTime"/> of this entity must be stored.
    /// <see cref="CreationTime"/> can be automatically set when saving <see cref="Entity"/> to database.
    /// 创建时间接口
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// 创建实体时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}