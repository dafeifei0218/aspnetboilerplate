using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Uow;

namespace Abp.Domain.Repositories
{
    /// <summary>
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// 带泛型的仓储接口
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on 实体</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity 实体的主键</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Select/Get/Query

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// <see cref="UnitOfWorkAttribute"/> attribute must be used to be able to call this method since this method
        /// returns IQueryable and it requires open database connection to use it.
        /// 获取全部实体。
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Used to get all entities.
        /// 获取全部实体。
        /// </summary>
        /// <returns>List of all entities 全部实体列表</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// Used to get all entities.
        /// 获取全部实体-异步。
        /// </summary>
        /// <returns>List of all entities 全部实体列表</returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// 获取全部实体。
        /// </summary>
        /// <param name="predicate">A condition to filter entities 条件</param>
        /// <returns>List of all entities 全部实体列表</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// 获取全部实体-异步。
        /// </summary>
        /// <param name="predicate">A condition to filter entities 条件</param>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to run a query over entire entities.
        /// <see cref="UnitOfWorkAttribute"/> attribute is not always necessary (as opposite to <see cref="GetAll"/>)
        /// if <paramref name="queryMethod"/> finishes IQueryable with ToList, FirstOrDefault etc..
        /// 获取单个实体。
        /// </summary>
        /// <typeparam name="T">Type of return value of this method</typeparam>
        /// <param name="queryMethod">This method is used to query over entities</param>
        /// <returns>Query result</returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        /// <summary>
        /// Gets an entity with given primary key.
        /// 获取一个给定主键的实体。
        /// </summary>
        /// <param name="id">Primary key of the entity to get 主键</param>
        /// <returns>Entity</returns>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given primary key.
        /// 获取一个给定主键的实体。
        /// </summary>
        /// <param name="id">Primary key of the entity to get 主键</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetAsync(TPrimaryKey id);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// 获取一个给定条件的实体。
        /// 如果没有实体或多个实体则抛出异常。
        /// </summary>
        /// <param name="predicate">Entity 条件</param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// 获取一个给定条件的实体。
        /// 如果没有实体或多个实体则抛出异常。
        /// </summary>
        /// <param name="predicate">Entity 条件</param>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// 获取一个给定主键的实体，当从没有时返回null，返回多个时会返回第一个。
        /// </summary>
        /// <param name="id">Primary key of the entity to get 主键</param>
        /// <returns>Entity or null 实体或null</returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// 异步获取一个给定主键的实体，当从没有时返回null，返回多个时会返回第一个。
        /// </summary>
        /// <param name="id">Primary key of the entity to get 主键</param>
        /// <returns>Entity or null 实体或null</returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// 获取一个给定条件的实体，当从没有时返回null，返回多个时会返回第一个。
        /// </summary>
        /// <param name="predicate">Predicate to filter entities 实体过滤条件</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// 异步获取一个给定条件的实体，当从没有时返回null，返回多个时会返回第一个。
        /// </summary>
        /// <param name="predicate">Predicate to filter entities 实体过滤条件</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Creates an entity with given primary key without database access.
        /// 获取一个给定条件的实体，
        /// 并不会从数据库中检索实体，但他会创建延迟执行所需的代理对象，
        /// 如果你只使用Id属性，实际上并不会检索实体，
        /// 它只有在你存取想要查询实体的某个属性时才会从数据库中查询实体。
        /// </summary>
        /// <param name="id">Primary key of the entity to load 主键</param>
        /// <returns>Entity 实体</returns>
        TEntity Load(TPrimaryKey id);

        #endregion

        #region Insert

        /// <summary>
        /// Inserts a new entity.
        /// 插入一个新实体。
        /// </summary>
        /// <param name="entity">Inserted entity 插入实体</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity.
        /// 异步插入一个新实体。
        /// </summary>
        /// <param name="entity">Inserted entity 插入实体</param>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// 插入一个新实体，并获取主键。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        /// <returns>Id of the entity 实体的主键</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// 异步插入一个新实体并获取主键。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        /// <returns>Id of the entity 实体的主键</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// 根据实体插入或更新一个实体。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// 异步根据实体插入或更新一个实体。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// 根据实体插入或更新一个实体，并获取主键。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        /// <returns>Id of the entity 实体的主键</returns>
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// 异步根据实体插入或更新一个实体，并获取主键。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        /// <returns>Id of the entity 实体的主键</returns>
        Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

        #endregion

        #region Update

        /// <summary>
        /// Updates an existing entity.
        /// 更新现有的实体。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity. 
        /// 异步更新现有的实体。
        /// </summary>
        /// <param name="entity">Entity 实体</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// 更新现有的实体。
        /// </summary>
        /// <param name="id">Id of the entity 实体的主键</param>
        /// <param name="updateAction">Action that can be used to change values of the entity 更新条件</param>
        /// <returns>Updated entity 更新的实体</returns>
        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        /// <summary>
        /// Updates an existing entity.
        /// 异步更新现有的实体。
        /// </summary>
        /// <param name="id">Id of the entity 实体的主键</param>
        /// <param name="updateAction">Action that can be used to change values of the entity 更新条件</param>
        /// <returns>Updated entity 更新的实体</returns>
        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// 删除实体。
        /// </summary>
        /// <param name="entity">Entity to be deleted 被删除的实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// 异步删除实体。
        /// </summary>
        /// <param name="entity">Entity to be deleted 被删除的实体</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity by primary key.
        /// 根据主键删除实体。
        /// </summary>
        /// <param name="id">Primary key of the entity 主键</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// Deletes an entity by primary key.
        /// 异步根据主键删除实体。
        /// </summary>
        /// <param name="id">Primary key of the entity 主键</param>
        Task DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// 删除多个实体。
        /// </summary>
        /// <param name="predicate">A condition to filter entities 条件</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// 删除多个实体。
        /// </summary>
        /// <param name="predicate">A condition to filter entities 条件</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregates

        /// <summary>
        /// Gets count of all entities in this repository.
        /// 获取所有实体的个数。
        /// </summary>
        /// <returns>Count of entities 实体计数</returns>
        int Count();

        /// <summary>
        /// Gets count of all entities in this repository.
        /// 异步获取所有实体的个数。
        /// </summary>
        /// <returns>Count of entities 实体计数</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// 根据条件获取所有实体的个数。
        /// </summary>
        /// <param name="predicate">A method to filter count 条件</param>
        /// <returns>Count of entities 实体计数</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// 异步根据条件获取所有实体的个数。
        /// </summary>
        /// <param name="predicate">A method to filter count 条件</param>
        /// <returns>Count of entities 实体计数</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// 获取所有实体的个数（如果返回值大于int.MaxValue）。
        /// </summary>
        /// <returns>Count of entities 实体计数</returns>
        long LongCount();

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// 异步获取所有实体的个数（如果返回值大于int.MaxValue）。
        /// </summary>
        /// <returns>Count of entities 实体计数</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// 根据条件获取所有实体的个数。
        /// </summary>
        /// <param name="predicate">A method to filter count 条件</param>
        /// <returns>Count of entities 实体计数</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// 异步根据条件获取所有实体的个数。
        /// </summary>
        /// <param name="predicate">A method to filter count 条件</param>
        /// <returns>Count of entities 实体计数</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
