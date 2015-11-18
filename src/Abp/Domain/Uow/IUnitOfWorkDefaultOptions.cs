using System;
using System.Collections.Generic;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Used to get/set default options for a unit of work.
    /// 工作单元默认选项接口
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Scope option.
        /// 范围选项
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// Should unit of works be transactional.
        /// Default: true.
        /// 是否事务，默认为：true
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for unit of works.
        /// 时间范围
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets isolation level of transaction.
        /// This is used if <see cref="IsTransactional"/> is true.
        /// 事务的隔离级别
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets list of all data filter configurations.
        /// 数据过滤器配置列表
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// Registers a data filter to unit of work system.
        /// 注册过滤器
        /// </summary>
        /// <param name="filterName">Name of the filter. 过滤器名称</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. 是否默认启用</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// Overrides a data filter definition.
        /// 覆盖过滤器
        /// </summary>
        /// <param name="filterName">Name of the filter. 过滤器名称</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. 是否默认启用</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}