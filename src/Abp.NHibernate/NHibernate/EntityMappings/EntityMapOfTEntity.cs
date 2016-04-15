using Abp.Domain.Entities;

namespace Abp.NHibernate.EntityMappings
{
    /// <summary>
    /// A shortcut of <see cref="EntityMap{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// һ����ݵ�ʵ��ӳ�䣬ʹ��(<see cref="int"/>)��Ϊ������
    /// </summary>
    /// <typeparam name="TEntity">Entity map ʵ��</typeparam>
    public abstract class EntityMap<TEntity> : EntityMap<TEntity, int> where TEntity : IEntity<int>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="tableName">Table name ����</param>
        protected EntityMap(string tableName)
            : base(tableName)
        {

        }
    }
}