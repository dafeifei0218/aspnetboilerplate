using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// ������ԪĬ������ѡ��
    /// </summary>
    internal class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// ����Χ
        /// </summary>
        public TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// �Ƿ�����Ĭ��Ϊ��true
        /// </summary>
        /// <inheritdoc/>
        public bool IsTransactional { get; set; }

        /// <summary>
        /// ��ʱʱ��
        /// </summary>
        /// <inheritdoc/>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// ����ĸ��뼶��
        /// </summary>
        /// <inheritdoc/>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters; }
        }
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// ע�������
        /// </summary>
        /// <param name="filterName">Name of the filter. ����������</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. �Ƿ�Ĭ������</param>
        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.FilterName == filterName))
            {
                throw new AbpException("There is already a filter with name: " + filterName);
            }

            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        /// <summary>
        /// ��д������
        /// </summary>
        /// <param name="filterName">Name of the filter. ����������</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. �Ƿ�Ĭ������</param>
        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            _filters.RemoveAll(f => f.FilterName == filterName);
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public UnitOfWorkDefaultOptions()
        {
            _filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
        }
    }
}