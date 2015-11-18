namespace Abp.Domain.Uow
{
    /// <summary>
    /// Used to get/set current <see cref="IUnitOfWork"/>. 
    /// 获取或设置当前工作单元提供者接口
    /// </summary>
    public interface ICurrentUnitOfWorkProvider
    {
        /// <summary>
        /// Gets/sets current <see cref="IUnitOfWork"/>.
        /// 获取或设置当前工作单元
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}