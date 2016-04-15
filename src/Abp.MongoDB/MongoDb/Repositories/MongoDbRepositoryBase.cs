using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Abp.MongoDb.Repositories
{
    /// <summary>
    /// Implements IRepository for MongoDB.
    /// MongoDb仓储基类。
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseProvider">Mongo数据库提供者</param>
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    /// <summary>
    /// Implements IRepository for MongoDB.
    /// MongoDb仓储基类。
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository 仓储的实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity 实体主键</typeparam>
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Mongo数据库提供者接口
        /// </summary>
        private readonly IMongoDatabaseProvider _databaseProvider;

        /// <summary>
        /// Mongo数据库
        /// </summary>
        protected MongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        /// <summary>
        /// Mongo集合
        /// </summary>
        protected MongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseProvider">Mongo数据库提供者</param>
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public override TEntity Get(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query); //TODO: What if no entity with id?
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query); //TODO: What if no entity with id?
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            Collection.Insert(entity);
            return entity;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override TEntity Update(TEntity entity)
        {
            Collection.Save(entity);
            return entity;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        public override void Delete(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            Collection.Remove(query);
        }
    }
}