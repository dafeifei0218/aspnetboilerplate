namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface ads <see cref="IDeletionAudited"/> to <see cref="IAudited"/> for a fully audited entity.
    /// ȫ�����ʵ��
    /// </summary>
    public interface IFullAudited : IAudited, IDeletionAudited
    {
        
    }
}