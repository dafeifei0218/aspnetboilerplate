using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 审计实体数据传输对象，主键为int
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityDto : AuditedEntityDto<int>
    {

    }
}