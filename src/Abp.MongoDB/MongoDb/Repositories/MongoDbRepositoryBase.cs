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
    /// MongoDb�ִ����ࡣ
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="databaseProvider">Mongo���ݿ��ṩ��</param>
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    /// <summary>
    /// Implements IRepository for MongoDB.
    /// MongoDb�ִ����ࡣ
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository �ִ���ʵ������</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity ʵ������</typeparam>
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Mongo���ݿ��ṩ�߽ӿ�
        /// </summary>
        private readonly IMongoDatabaseProvider _databaseProvider;

        /// <summary>
        /// Mongo���ݿ�
        /// </summary>
        protected MongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        /// <summary>
        /// Mongo����
        /// </summary>
        protected MongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="databaseProvider">Mongo���ݿ��ṩ��</param>
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        /// <summary>
        /// ��ȡȫ��
        /// </summary>
        /// <returns></returns>
        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        public override TEntity Get(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query); //TODO: What if no entity with id?
        }

        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query); //TODO: What if no entity with id?
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            Collection.Insert(entity);
            return entity;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public override TEntity Update(TEntity entity)
        {
            Collection.Save(entity);
            return entity;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="id">����</param>
        public override void Delete(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            Collection.Remove(query);
        }
    }
}