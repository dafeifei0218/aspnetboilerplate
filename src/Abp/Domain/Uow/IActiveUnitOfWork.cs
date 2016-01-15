using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This interface is used to work with active unit of work.
    /// This interface can not be injected.
    /// Use <see cref="IUnitOfWorkManager"/> instead.
    /// 活动工作单元接口。
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// This event is raised when this UOW is successfully completed.
        /// UOW成功时执行此事件。
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// This event is raised when this UOW is failed.
        /// UOW失败时执行此事件。
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// This event is raised when this UOW is disposed.
        /// UOW设置时执行此事件。
        /// </summary>
        event EventHandler Disposed;
        
        /// <summary>
        /// Gets if this unit of work is transactional.
        /// 工作单元设置
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        /// Gets data filter configurations for this unit of work.
        /// 数据过滤配置
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// Is this UOW disposed?
        /// 是否UOW的设置
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// This method may be called to apply changes whenever needed.
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// since all changes saved at end of a unit of work automatically.
        /// 保存变更
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// This method may be called to apply changes whenever needed.
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// since all changes saved at end of a unit of work automatically.
        /// 保存变更异步
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Disables one or more data filters.
        /// Does nothing for a filter if it's already disabled. 
        /// Use this method in a using statement to re-enable filters if needed.
        /// 禁用一个或多个数据筛选器
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters. 筛选器名称集合</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the disable effect.</returns>
        IDisposable DisableFilter(params string[] filterNames);

        /// <summary>
        /// Enables one or more data filters.
        /// Does nothing for a filter if it's already enabled.
        /// Use this method in a using statement to re-disable filters if needed.
        /// 启用一个或多个数据筛选器
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters. 筛选器名称集合</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the enable effect.</returns>
        IDisposable EnableFilter(params string[] filterNames);

        /// <summary>
        /// Checks if a filter is enabled or not.
        /// 是否启用筛选器
        /// </summary>
        /// <param name="filterName">Name of the filter. <see cref="AbpDataFilters"/> for standard filters. 筛选器名称</param>
        bool IsFilterEnabled(string filterName);

        /// <summary>
        /// Sets (overrides) value of a filter parameter.
        /// 设置筛选器参数值
        /// </summary>
        /// <param name="filterName">Name of the filter 过滤器名称</param>
        /// <param name="parameterName">Parameter's name 参数名称</param>
        /// <param name="value">Value of the parameter to be set 参数值</param>
        IDisposable SetFilterParameter(string filterName, string parameterName, object value);
    }
}