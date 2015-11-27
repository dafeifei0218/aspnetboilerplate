namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Defines common properties for entity based DTOs.
    /// 实体数据传输对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键</typeparam>
    public interface IEntityDto<TPrimaryKey> : IDto
    {
        /// <summary>
        /// Id of the entity.
        /// 实体主键
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}