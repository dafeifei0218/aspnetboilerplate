namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IAudited"/> interface for user.
    /// ��ƽӿ�
    /// </summary>
    /// <typeparam name="TUser">Type of the user �û�����</typeparam>
    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}