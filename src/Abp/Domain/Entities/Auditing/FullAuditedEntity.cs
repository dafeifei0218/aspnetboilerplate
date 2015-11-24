using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// ȫ�����ʵ�壬����Ϊint
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntity : FullAuditedEntity<int>
    {

    }
}