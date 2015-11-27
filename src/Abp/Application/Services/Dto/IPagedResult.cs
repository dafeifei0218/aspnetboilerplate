namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to return a page of items to clients.
    /// 分页的结果接口
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="IListResult{T}.Items"/> list</typeparam>
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {

    }
}