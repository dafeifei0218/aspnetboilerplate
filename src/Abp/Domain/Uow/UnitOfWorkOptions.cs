using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Unit of work options.
    /// 工作单元选项
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// Scope option.
        /// 范围选项
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?
        /// Uses default value if not supplied.
        /// 是否事务
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.
        /// Uses default value if not supplied.
        /// 时间范围
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// Uses default value if not supplied.
        /// 事务的隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// This option should be set to <see cref="TransactionScopeAsyncFlowOption.Enabled"/>
        /// if unit of work is used in an async scope.
        /// 异步事务流范围选项
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }

        /// <summary>
        /// Can be used to enable/disable some filters. 
        /// 数据过滤器配置列表
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; private set; }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkOptions"/> object.
        /// 构造函数
        /// </summary>
        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }

        /// <summary>
        /// 填充默认值
        /// </summary>
        /// <param name="defaultOptions">工作单元默认选项</param>
        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            //TODO: Do not change options object..?

            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }

            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }

            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }
        }
    }
}