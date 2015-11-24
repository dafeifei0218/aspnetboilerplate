namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IFullAudited"/> interface for user.
    /// ȫ�����ʵ��
    /// </summary>
    /// <typeparam name="TUser">Type of the user �û�����</typeparam>
    public interface IFullAudited<TUser> : IFullAudited, IDeletionAudited<TUser> 
        where TUser : IEntity<long>
    {

    }
}