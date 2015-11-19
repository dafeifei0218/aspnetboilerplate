namespace Abp.Domain.Entities
{
    /// <summary>
    /// A shortcut of <see cref="IEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 实体接口，主键为int
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }
}