using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// ȫ�����ʵ�����ݴ����������Ϊint
    /// </summary>
    [Serializable]
    public class FullAuditedEntityDto : FullAuditedEntityDto<int>
    {

    }
}