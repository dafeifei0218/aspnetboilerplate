using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Session;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// ������Ԫ����
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        /// <summary>
        /// Ψһ�ı�ʶID
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// ��㹤����Ԫ
        /// </summary>
        public IUnitOfWork Outer { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        /// <inheritdoc/>
        public event EventHandler Completed;

        /// <summary>
        /// ʧ��
        /// </summary>
        /// <inheritdoc/>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// ����
        /// </summary>
        /// <inheritdoc/>
        public event EventHandler Disposed;

        /// <summary>
        /// ������Ԫѡ��
        /// </summary>
        /// <inheritdoc/>
        public UnitOfWorkOptions Options { get; private set; }

        /// <summary>
        /// ɸѡ������
        /// </summary>
        /// <inheritdoc/>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters.ToImmutableList(); }
        }

        /// <summary>
        /// ɸѡ��
        /// </summary>
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// Gets default UOW options.
        /// ��ȡ������Ԫѡ��
        /// </summary>
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; private set; }

        /// <summary>
        /// Gets a value indicates that this unit of work is disposed or not.
        /// �Ƿ��ͷţ���ȡһ��ֵ����ֵָʾ�ù�����Ԫ�Ƿ��ͷ�
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Reference to current ABP session.
        /// Abp�Ự
        /// </summary>
        public IAbpSession AbpSession { private get; set; }

        /// <summary>
        /// Is <see cref="Begin"/> method called before?
        /// �Ƿ�ʼ֮ǰ
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        /// Is <see cref="Complete"/> method called before?
        /// �Ƿ����֮ǰ
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        /// Is this unit of work successfully completed.
        /// �ɹ�
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// A reference to the exception if this unit of work failed.
        /// �쳣
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        protected UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions)
        {
            DefaultOptions = defaultOptions;

            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();
            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// ��ʼ������Ԫ.
        /// </summary>
        /// <inheritdoc/>
        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            //��ֹBegin����ε���
            PreventMultipleBegin();
            Options = options; //TODO: Do not set options like that, instead make a copy?

            //��������
            SetFilters(options.FilterOverrides);

            //���󷽷�������ʵ��
            BeginUow();
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <inheritdoc/>
        public abstract void SaveChanges();

        /// <summary>
        /// ������-�첽
        /// </summary>
        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        /// <summary>
        /// ����ɸѡ��
        /// </summary>
        /// <param name="filterNames">���������Ƽ���</param>
        /// <inheritdoc/>
        public IDisposable DisableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var disabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(filterName, false);
                }
            }

            disabledFilters.ForEach(ApplyDisableFilter);

            return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));
        }

        /// <summary>
        /// ���ù�����
        /// </summary>
        /// <param name="filterNames">���������Ƽ���</param>
        /// <inheritdoc/>
        public IDisposable EnableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var enabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(filterName, true);
                }
            }

            enabledFilters.ForEach(ApplyEnableFilter);

            return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        }

        /// <summary>
        /// �Ƿ����ù�����
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <inheritdoc/>
        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        /// <summary>
        /// ���ù���������
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <param name="parameterName">��������</param>
        /// <param name="value">ֵ</param>
        /// <inheritdoc/>
        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            var filterIndex = GetFilterIndex(filterName);

            var newfilter = new DataFilterConfiguration(_filters[filterIndex]);

            //Store old value
            object oldValue = null;
            var hasOldValue = newfilter.FilterParameters.ContainsKey(filterName);
            if (hasOldValue)
            {
                oldValue = newfilter.FilterParameters[filterName];
            }

            newfilter.FilterParameters[parameterName] = value;

            _filters[filterIndex] = newfilter;

            ApplyFilterParameterValue(filterName, parameterName, value);

            return new DisposeAction(() =>
            {
                //Restore old value
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });
        }

        /// <summary>
        /// �ɹ�
        /// </summary>
        /// <inheritdoc/>
        public void Complete()
        {
            //��ֹComplete����ε���
            PreventMultipleComplete();
            try
            {
                //���󷽷�������ʵ��
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// �ɹ�-�첽
        /// </summary>
        /// <inheritdoc/>
        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// �ͷ�
        /// </summary>
        /// <inheritdoc/>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

        /// <summary>
        /// Should be implemented by derived classes to start UOW.
        /// ��ʼ������Ԫ
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// ��ɹ�����Ԫ
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// �첽��ɹ�����Ԫ
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// �ͷŹ�����Ԫ
        /// </summary>
        protected abstract void DisposeUow();

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to disable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// Ӧ�ý��ù�����
        /// </summary>
        /// <param name="filterName">Filter name ����������</param>
        protected virtual void ApplyDisableFilter(string filterName)
        {
            throw new NotImplementedException("DisableFilter is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to enable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// Ӧ�����ù�����
        /// </summary>
        /// <param name="filterName">Filter name ����������</param>
        protected virtual void ApplyEnableFilter(string filterName)
        {
            throw new NotImplementedException("EnableFilter is not implemented for " + GetType().FullName);
        }


        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to set a parameter's value.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// Ӧ�ù���������ֵ
        /// </summary>
        /// <param name="filterName">Filter name ����������</param>
        /// <param name="parameterName">��������</param>
        /// <param name="value">ֵ</param>
        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            throw new NotImplementedException("SetFilterParameterValue is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// ���ô�������¼�
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// ���ô���ʧ���¼�
        /// </summary>
        /// <param name="exception">Exception that cause failure �쳣</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// ���ô����ͷ��¼�
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }

        /// <summary>
        /// ��ֹ��ο�ʼ������Ԫ
        /// </summary>
        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                //��һ������Ԫ�Ѿ���ʼ�����ܵ�������������ֹһ�Ρ�
                throw new AbpException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        /// <summary>
        /// ��ֹ�����ɹ�����Ԫ
        /// </summary>
        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new AbpException("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }

        /// <summary>
        /// ���ù�����
        /// </summary>
        /// <param name="filterOverrides">���ݹ���������</param>
        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (var i = 0; i < _filters.Count; i++)
            {
                var filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }

            if (!AbpSession.UserId.HasValue || AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                ChangeFilterIsEnabledIfNotOverrided(filterOverrides, AbpDataFilters.MustHaveTenant, false);
            }
        }

        /// <summary>
        /// ���û����д�����Ĺ������Ƿ�����
        /// </summary>
        /// <param name="filterOverrides">���ݹ���������</param>
        /// <param name="filterName">����������</param>
        /// <param name="isEnabled">�Ƿ�����</param>
        private void ChangeFilterIsEnabledIfNotOverrided(List<DataFilterConfiguration> filterOverrides, string filterName, bool isEnabled)
        {
            if (filterOverrides.Any(f => f.FilterName == filterName))
            {
                return;
            }

            var index = _filters.FindIndex(f => f.FilterName == filterName);
            if (index < 0)
            {
                return;
            }

            if (_filters[index].IsEnabled == isEnabled)
            {
                return;
            }

            _filters[index] = new DataFilterConfiguration(filterName, isEnabled);
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="filterName">����������</param>
        private DataFilterConfiguration GetFilter(string filterName)
        {
            var filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
            if (filter == null)
            {
                throw new AbpException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filter;
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <param name="filterName">����������</param>
        private int GetFilterIndex(string filterName)
        {
            var filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
            if (filterIndex < 0)
            {
                throw new AbpException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filterIndex;
        }
    }
}