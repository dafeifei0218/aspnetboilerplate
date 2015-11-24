using Abp.Domain.Entities;

namespace Abp.Domain.Repositories
{
    /// <summary>
    /// A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// 仓储接口，主键为int
    /// </summary>
    /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}
