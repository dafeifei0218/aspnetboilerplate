namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IModificationAudited"/> interface for user.
    /// �޸���ƽӿ�
    /// </summary>
    /// <typeparam name="TUser">Type of the user �û�ʵ��</typeparam>
    public interface IModificationAudited<TUser> : IModificationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// ����޸��û�ʵ��
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}