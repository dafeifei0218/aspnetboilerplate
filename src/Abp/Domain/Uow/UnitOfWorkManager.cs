using System;
using System.Transactions;
using Abp.Dependency;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Unit of work manager.
    /// 工作单元管理类
    /// </summary>
    /// Unit Of Work大致运行流程如下
    /// 1. UOW拦截器被注入到需要UOW的类中。
    /// 2. ABP执行标注了UnitOfWork特性的方法时。
    /// UOW拦截器以begin()->realAction()->complete()->dispose()顺序执行， 其中realAction是被调用的真实业务操作。 
    /// UOW拦截器是通过using这种方式调用IUnitOfWork的某个具体实现，这就确保begin 和 dispose也总是会被执行的。 
    /// 这里需要注意complete却不一定会被执行，比如在complete方法被调用前方法的执行产生了异常。
    /// 3. 当执行一连串的操作时（A方法->B方法->C方法，假设这三个方法都标注了UnitOfWork特性），
    /// ABP在执行A方法前会创建整个过程中唯一的IUnitOfWork对象，该对象会启动.NET事务。
    /// 在执行到B，C方法只会创建InnerUnitOfWorkCompleteHandle。 
    /// InnerUnitOfWorkCompleteHandle与IUnitOfWork对象的差异在于它不会创建真实的事务。
    /// 但ABP会调用其complete，以告知ABP其对应的方法以成功完成，可以提交事务.
    /// 4. 事务可以回滚的关键关键在于IUnitOfWork对象在被dispose时候会检查complete方法有没有被执行，
    /// 没有的话就认为这个UOW标注的方法没有顺利完成，从而导致事务的回滚操作。
    /// 5. 整个事务的提交是通过第一个UOW（也是唯一个）的complete方法执行时提交的。
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
        /// 这边可以看出只有一个IUnitOfWork对象会被创建, 而且由于这个对象是通过容器直接resolve的，
        /// 那么ABP怎么知道该通过resolve得到什么样的实例呢？是EfUnitOfWork?还是MongoDbUnitOfWork？还是MemoryDbUnitOfWork？还是NhUnitOfWork？
        /// 答案是ABP不知道，ABP作者假设你只会用其中一个模块，所以如果你把这四个module都加入到你的项目中，结果是不可预知的。
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