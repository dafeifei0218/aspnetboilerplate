using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.MemoryDb.Repositories
{
    /// <summary>
    /// �ڴ�ִ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    //TODO: Implement thread-safety..?
    public class MemoryRepository<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// �ڴ����ݿ��ṩ��
        /// </summary>
        private readonly IMemoryDatabaseProvider _databaseProvider;
        /// <summary>
        /// �ڴ����ݿ�
        /// </summary>
        protected MemoryDatabase Database { get { return _databaseProvider.Database; } }

        /// <summary>
        /// ���б�
        /// </summary>
        protected List<TEntity> Table { get { return Database.Set<TEntity>(); } }

        /// <summary>
        /// �ڴ�����ͨ��
        /// </summary>
        private readonly MemoryPrimaryKeyGenerator<TPrimaryKey> _primaryKeyGenerator;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="databaseProvider">�ڴ����ݿ��ṩ��</param>
        public MemoryRepository(IMemoryDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            _primaryKeyGenerator = new MemoryPrimaryKeyGenerator<TPrimaryKey>();
        }

        /// <summary>
        /// ��ȡȫ��
        /// </summary>
        /// <returns></returns>
        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            if (entity.IsTransient())
            {
                entity.Id = _primaryKeyGenerator.GetNext();
            }

            Table.Add(entity);
            return entity;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public override TEntity Update(TEntity entity)
        {
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, entity.Id));
            if (index >= 0)
            {
                Table[index] = entity;
            }

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
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, id));
            if (index >= 0)
            {
                Table.RemoveAt(index);
            }
        }
    }
}