using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Unit of work options.
    /// ������Ԫѡ��
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// Scope option.
        /// ��Χѡ��
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?
        /// Uses default value if not supplied.
        /// �Ƿ�����
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.
        /// Uses default value if not supplied.
        /// ʱ�䷶Χ
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// Uses default value if not supplied.
        /// ����ĸ��뼶��
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// This option should be set to <see cref="TransactionScopeAsyncFlowOption.Enabled"/>
        /// if unit of work is used in an async scope.
        /// �첽��������Χѡ��
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }

        /// <summary>
        /// Can be used to enable/disable some filters. 
        /// ���ݹ����������б�
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; private set; }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkOptions"/> object.
        /// ���캯��
        /// </summary>
        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }

        /// <summary>
        /// ���Ĭ��ֵ
        /// </summary>
        /// <param name="defaultOptions">������ԪĬ��ѡ��</param>
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