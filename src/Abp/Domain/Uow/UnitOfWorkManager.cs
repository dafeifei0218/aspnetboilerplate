using System;
using System.Transactions;
using Abp.Dependency;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Unit of work manager.
    /// 工作单元管理类
    /// </summary>
    internal class UnitOfWorkManager : IUnitOfWorkManager, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly IUnitOfWorkDefaultOptions _defaultOptions;

        /// <summary>
        /// 当前工作单元
        /// </summary>
        public IActiveUnitOfWork Current
        {
            get { return _currentUnitOfWorkProvider.Current; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">IOC控制反转解析器</param>
        /// <param name="currentUnitOfWorkProvider">当前工作单元提供者</param>
        /// <param name="defaultOptions">工作单元默认选项</param>
        public UnitOfWorkManager(
            IIocResolver iocResolver,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider,
            IUnitOfWorkDefaultOptions defaultOptions)
        {
            _iocResolver = iocResolver;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _defaultOptions = defaultOptions;
        }

        /// <summary>
        /// 开始工作单元
        /// </summary>
        /// <returns></returns>
        public IUnitOfWorkCompleteHandle Begin()
        {
            return Begin(new UnitOfWorkOptions());
        }

        /// <summary>
        /// 开始工作单元
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <returns></returns>
        public IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope)
        {
            return Begin(new UnitOfWorkOptions { Scope = scope });
        }

        /// <summary>
        /// 开始工作单元
        /// </summary>
        /// <param name="options">工作单元选项</param>
        /// <returns></returns>
        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options)
        {
            //为未赋值的参数设置默认值
            options.FillDefaultsForNonProvidedOptions(_defaultOptions);

            if (options.Scope == TransactionScopeOption.Required && _currentUnitOfWorkProvider.Current != null)
            {
                //如果当前Scope的设置为Required（而非RequiredNew），并且当前已存在工作单元，那么久返回下面这样的一个对象
                return new InnerUnitOfWorkCompleteHandle();
            }

            //走到这里，表示需要一个新的工作单元，通过IoC创建IUnitOfWork实现对象，然后开始工作单元，并设置此工作单元为当前工作单元
            var uow = _iocResolver.Resolve<IUnitOfWork>();

            uow.Completed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Failed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Disposed += (sender, args) =>
            {
                _iocResolver.Release(uow);
            };

            uow.Begin(options);

            _currentUnitOfWorkProvider.Current = uow;

            return uow;
        }
    }
}