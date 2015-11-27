using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    ///  A shortcut of <see cref="CreationAuditedEntityDto"/> for most used primary key type (<see cref="int"/>).
    /// 创建审计实体数据传输对象，主键为int
    /// </summary>
    [Serializable]
    public abstract class CreationAuditedEntityDto : CreationAuditedEntityDto<int>
    {
        
    }
}