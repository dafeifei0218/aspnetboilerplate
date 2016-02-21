using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
    /// ɾ����ƽӿ�
    /// </summary>
    //public interface IDeletionAudited : ISoftDelete
    public interface IDeletionAudited : IHasDeletionTime 
    {
        /// <summary>
        /// Which user deleted this entity?
        /// ʵ��ɾ�����û�Id
        /// </summary>
        long? DeleterUserId { get; set; }

        ///// <summary>
        ///// Deletion time of this entity.
        ///// ʵ��ɾ����ʱ��
        ///// </summary>
        //DateTime? DeletionTime { get; set; }
    }
}