namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IDeletionAudited"/> interface for user.
    /// ɾ����ƽӿ�
    /// </summary>
    /// <typeparam name="TUser">Type of the user �û�����</typeparam>
    public interface IDeletionAudited<TUser> : IDeletionAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the deleter user of this entity.
        /// ʵ��ɾ�����û�
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}