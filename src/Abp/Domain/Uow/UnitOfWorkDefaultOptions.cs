using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// 工作单元默认配置选项
    /// </summary>
    internal class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// 事务范围
        /// </summary>
        public TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// 是否事务，默认为：true
        /// </summary>
        /// <inheritdoc/>
        public bool IsTransactional { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        /// <inheritdoc/>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 事务的隔离级别
        /// </summary>
        /// <inheritdoc/>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 过滤器集合
        /// </summary>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters; }
        }
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// 注册过滤器
        /// </summary>
        /// <param name="filterName">Name of the filter. 过滤器名称</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. 是否默认启用</param>
        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.FilterName == filterName))
            {
                throw new AbpException("There is already a filter with name: " + filterName);
            }

            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        /// <summary>
        /// 重写过滤器
        /// </summary>
        /// <param name="filterName">Name of the filter. 过滤器名称</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. 是否默认启用</param>
        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            _filters.RemoveAll(f => f.FilterName == filterName);
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UnitOfWorkDefaultOptions()
        {
            _filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
        }
    }
}