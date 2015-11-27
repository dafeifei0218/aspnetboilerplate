using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 全部审计实体数据传输对象，主键为int
    /// </summary>
    [Serializable]
    public class FullAuditedEntityDto : FullAuditedEntityDto<int>
    {

    }
}