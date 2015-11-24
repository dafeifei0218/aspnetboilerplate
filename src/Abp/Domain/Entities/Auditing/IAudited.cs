namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities which must be audited.
    /// Related properties automatically set when saving/updating <see cref="Entity"/> objects.
    /// Éó¼Æ½Ó¿Ú
    /// </summary>
    public interface IAudited : ICreationAudited, IModificationAudited
    {

    }
}