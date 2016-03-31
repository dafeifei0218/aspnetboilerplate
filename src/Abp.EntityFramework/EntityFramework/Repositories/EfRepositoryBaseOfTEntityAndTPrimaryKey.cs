using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// Implements IRepository for Entity Framework.
    /// EntityFramework仓储实现
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <see cref="TEntity"/>. 数据上下文</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository 实体</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity 主键</typeparam>
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        #region Select/Get/Query

        /// <summary>
        /// Gets EF DbContext object.
        /// 数据上下文
        /// </summary>
        protected virtual TDbContext Context { get { return _dbContextProvider.DbContext; } }

        /// <summary>
        /// Gets DbSet for given entity.
        /// 获取DbSet实体
        /// </summary>
        protected virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }

        /// <summary>
        /// 数据上下文提供者
        /// </summary>
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        /// <summary>
        /// Constructor
        /// 构造函数
        /// </summary>
        /// <param name="dbContextProvider">数据上下文提供者</param>
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        /// <summary>
        /// 获取全部实体
        /// </summary>
        /// <returns></returns>
        public override IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        /// <summary>
        /// 获取全部实体-异步
        /// </summary>
        /// <returns></returns>
        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        /// <summary>
        /// 获取全部实体-异步
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 获取一个给定条件的实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        /// <summary>
        /// 异步获取一个给定主键的实体，当从没有时返回null，返回多个时会返回第一个。
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 异步获取一个给定主键的实体，当从没有时返回null，返回多个时会返回第一个。
        /// </summary>
        /// <param name="predicate">实体过滤条件</param>
        /// <returns></returns>
        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        #endregion
        
        #region Insert

        /// <summary>
        /// 插入一个新实体。
        /// </summary>
        /// <param name="entity">插入实体</param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }

        /// <summary>
        /// 异步插入一个新实体。
        /// </summary>
        /// <param name="entity">插入实体</param>
        /// <returns></returns>
        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Table.Add(entity));
        }

        /// <summary>
        /// 插入一个新实体，并获取主键。
        /// </summary>
        /// <param name="entity">插入实体</param>
        /// <returns></returns>
        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        /// <summary>
        /// 异步插入一个新实体并获取主键。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }

        /// <summary>
        /// 根据实体插入或更新一个实体，并获取主键。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        /// <summary>
        /// 异步根据实体插入或更新一个实体，并获取主键。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }
        
        #endregion

        #region Update

        /// <summary>
        /// 更新现有的实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// 异步更新现有的实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="entity">实体</param>
        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);

            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
            }
            else
            {
                Table.Remove(entity);
            }
        }

        /// <summary>
        /// 根据主键删除实体。
        /// </summary>
        /// <param name="id">主键</param>
        public override void Delete(TPrimaryKey id)
        {
            var entity = Table.Local.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
            if (entity == null)
            {
                entity = FirstOrDefault(id);
                if (entity == null)
                {
                    return;
                }
            }

            Delete(entity);
        }

        #endregion

        #region Aggregates

        /// <summary>
        /// 异步获取所有实体的个数。
        /// </summary>
        /// <returns></returns>
        public override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        /// <summary>
        /// 根据条件获取所有实体的个数。
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        /// <summary>
        /// 异步获取所有实体的个数（如果返回值大于int.MaxValue）。
        /// </summary>
        /// <returns></returns>
        public override async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        /// <summary>
        /// 根据条件获取所有实体的个数。
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }
    }
}
