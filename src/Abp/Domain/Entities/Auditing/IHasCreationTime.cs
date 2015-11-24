using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// An entity can implement this interface if <see cref="CreationTime"/> of this entity must be stored.
    /// <see cref="CreationTime"/> can be automatically set when saving <see cref="Entity"/> to database.
    /// ����ʱ��ӿ�
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// ����ʵ��ʱ��
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}