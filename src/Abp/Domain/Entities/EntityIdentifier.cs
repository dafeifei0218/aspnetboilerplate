using System;

namespace Abp.Domain.Entities
{
    /// <summary>
    /// Used to identify an entity.
    /// Can be used to store an entity <see cref="Type"/> and <see cref="Id"/>.
    /// ʵ���ʶ����
    /// ����ʶ��һ��ʵ�塣
    /// ���Դ洢һ��ʵ��<see cref="Type"/>���ͺ�<see cref="Id"/>Id��
    /// </summary>
    [Serializable]
    public class EntityIdentifier
    {
        /// <summary>
        /// Entity Type.
        /// ʵ������
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Entity's Id.
        /// ʵ��Id
        /// </summary>
        public object Id { get; private set; }

        /// <summary>
        /// Added for serialization purposes.
        /// ���캯�������������л���Ŀ�ġ�
        /// </summary>
        private EntityIdentifier()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityIdentifier"/> class.
        /// ��ʼ��һ���µ�<see cref="EntityIdentifier"/>ʵ���ʶ����
        /// </summary>
        /// <param name="type">Entity type. ʵ������</param>
        /// <param name="id">Id of the entity. ʵ��Id</param>
        public EntityIdentifier(Type type, object id)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Type = type;
            Id = id;
        }
    }
}