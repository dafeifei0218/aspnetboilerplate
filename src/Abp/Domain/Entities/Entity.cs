using System;

namespace Abp.Domain.Entities
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// ʵ�壬����Ϊint
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {

    }
}