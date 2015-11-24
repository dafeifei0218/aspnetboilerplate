namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="ICreationAudited"/> interface for user.
    /// ������ƽӿ�
    /// </summary>
    /// <typeparam name="TUser">Type of the user �û�����</typeparam>
    public interface ICreationAudited<TUser> : ICreationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// �����û�
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}