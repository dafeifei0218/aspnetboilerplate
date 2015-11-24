using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// ���ʵ�壬����Ϊint
    /// </summary>
    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>
    {

    }
}