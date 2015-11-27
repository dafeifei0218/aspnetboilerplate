namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="IEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 实体数据传输对象，主键为int
    /// </summary>
    public interface IEntityDto : IEntityDto<int>
    {

    }
}