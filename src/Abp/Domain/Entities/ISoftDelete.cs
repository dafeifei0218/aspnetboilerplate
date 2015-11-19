namespace Abp.Domain.Entities
{
    /// <summary>
    /// Used to standardize soft deleting entities.
    /// Soft-delete entities are not actually deleted,
    /// marked as IsDeleted = true in the database,
    /// but can not be retrieved to the application.
    /// 软删除接口
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// 是否是删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
