namespace Abp.Domain.Entities
{
    /// <summary>
    /// Some usefull extension methods for Entities.
    /// 实体静态扩展类
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Check if this Entity is null of marked as deleted.
        /// 是否是Null或删除
        /// </summary>
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }
    }
}