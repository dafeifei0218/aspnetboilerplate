using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 审计实体，主键为int
    /// </summary>
    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>
    {

    }
}