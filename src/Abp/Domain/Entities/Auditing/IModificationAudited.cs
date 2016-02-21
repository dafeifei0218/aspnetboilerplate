using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified lastly).
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// �޸���ƽӿڡ�
    /// </summary>
    public interface IModificationAudited : IHasModificationTime
    {
        ///// <summary>
        ///// The last time of modification.
        ///// ����޸�ʵ���ʱ�䡣
        ///// </summary>
        //DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user for this entity.
        /// ����޸�ʵ��ĸ��û���
        /// </summary>
        long? LastModifierUserId { get; set; }
    }
}