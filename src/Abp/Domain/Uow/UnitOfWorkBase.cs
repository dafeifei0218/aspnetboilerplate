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
    /// 工作单元基类
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        /// <summary>
        /// 唯一的标识ID
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 外层工作单元
        /// </summary>
        public IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 完成
        /// </summary>
        /// <inheritdoc/>
        public event EventHandler Completed;

        /// <summary>
        /// 失败
        /// </summary>
        /// <inheritdoc/>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 处理
        /// </summary>
        /// <inheritdoc/>
        public event EventHandler Disposed;

        /// <summary>
        /// 工作单元选项
        /// </summary>
        /// <inheritdoc/>
        public UnitOfWorkOptions Options { get; private set; }

        /// <summary>
        /// 筛选器集合
        /// </summary>
        /// <inheritdoc/>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters.ToImmutableList(); }
        }

        /// <summary>
        /// 筛选器
        /// </summary>
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// Gets default UOW options.
        /// 获取工作单元选项
        /// </summary>
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; private set; }

        /// <summary>
        /// Gets a value indicates that this unit of work is disposed or not.
        /// 是否释放，获取一个值，该值指示该工作单元是否释放
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Reference to current ABP session.
        /// Abp会话
        /// </summary>
        public IAbpSession AbpSession { private get; set; }

        /// <summary>
        /// Is <see cref="Begin"/> method called before?
        /// 是否开始之前
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        /// Is <see cref="Complete"/> method called before?
        /// 是否完成之前
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        /// Is this unit of work successfully completed.
        /// 成功
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// A reference to the exception if this unit of work failed.
        /// 异常
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        protected UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions)
        {
            DefaultOptions = defaultOptions;

            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();
            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// 开始工作单元.
        /// </summary>
        /// <inheritdoc/>
        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            //防止Begin被多次调用
            PreventMultipleBegin();
            Options = options; //TODO: Do not set options like that, instead make a copy?

            //过滤配置
            SetFilters(options.FilterOverrides);

            //抽象方法，子类实现
            BeginUow();
        }

        /// <summary>
        /// 保存变更
        /// </summary>
        /// <inheritdoc/>
        public abstract void SaveChanges();

        /// <summary>
        /// 保存变更-异步
        /// </summary>
        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        /// <summary>
        /// 禁用筛选器
        /// </summary>
        /// <param name="filterNames">过滤器名称集合</param>
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
        /// 启用过滤器
        /// </summary>
        /// <param name="filterNames">过滤器名称集合</param>
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
        /// 是否启用过滤器
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        /// <inheritdoc/>
        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        /// <summary>
        /// 设置过滤器参数
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">值</param>
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
        /// 成功
        /// </summary>
        /// <inheritdoc/>
        public void Complete()
        {
            //防止Complete被多次调用
            PreventMultipleComplete();
            try
            {
                //抽象方法，子类实现
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
        /// 成功-异步
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
        /// 释放
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
        /// 开始工作单元
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// 完成工作单元
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// 异步完成工作单元
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// 释放工作单元
        /// </summary>
        protected abstract void DisposeUow();

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to disable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// 应用禁用过滤器
        /// </summary>
        /// <param name="filterName">Filter name 过滤器名称</param>
        protected virtual void ApplyDisableFilter(string filterName)
        {
            throw new NotImplementedException("DisableFilter is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to enable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// 应用启用过滤器
        /// </summary>
        /// <param name="filterName">Filter name 过滤器名称</param>
        protected virtual void ApplyEnableFilter(string filterName)
        {
            throw new NotImplementedException("EnableFilter is not implemented for " + GetType().FullName);
        }


        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to set a parameter's value.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// 应用过滤器参数值
        /// </summary>
        /// <param name="filterName">Filter name 过滤器名称</param>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">值</param>
        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            throw new NotImplementedException("SetFilterParameterValue is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// 调用触发完成事件
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// 调用触发失败事件
        /// </summary>
        /// <param name="exception">Exception that cause failure 异常</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// 调用触发释放事件
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }

        /// <summary>
        /// 防止多次开始工作单元
        /// </summary>
        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                //这一工作单元已经开始。不能调用启动方法不止一次。
                throw new AbpException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        /// <summary>
        /// 防止多次完成工作单元
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
        /// 设置过滤器
        /// </summary>
        /// <param name="filterOverrides">数据过滤器配置</param>
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
        /// 如果没有重写，更改过滤器是否启用
        /// </summary>
        /// <param name="filterOverrides">数据过滤器配置</param>
        /// <param name="filterName">过滤器名称</param>
        /// <param name="isEnabled">是否启用</param>
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
        /// 获取过滤器
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
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
        /// 获取过滤器索引
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
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