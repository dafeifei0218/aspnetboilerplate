using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;
using Abp.Dependency;
using Castle.Core;
using Castle.Core.Logging;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// CallContext implementation of <see cref="ICurrentUnitOfWorkProvider"/>. 
    /// This is the default implementation.
    /// 调用上下文当前工作单元提供者
    /// </summary>
    /// CallContextCurrentUnitOfWorkProvider的主要功能其实只有一个：通过current返回当前UOW环境下的UOW实例。
    /// 一般思路是：将IUnitOfWork对象定义为实例变量或者是类变量。 但是两者事实上都不可行。
    /// 如果定义为类变量，那就会面临线程安全的问题，解决方式无非加锁，但会导致并发能力下降，ABP是web框架，因为锁导致并发能力下降是不能接受的。
    /// 如果定义为实例变量，在同一线程其他地方resolve CallContextCurrentUnitOfWorkProvider这个实例的时候都会得到一个新的实例，新的实例下current自然是NULL.
    /// ABP的做法是：线程逻辑上下文+线程安全的Dictinoray容器。
    /// 线程逻辑上下文用于存储UOW实例的key, 而线程逻辑上下文对于本线程是全局可访问的，而同时具有天然的隔离性。这就确保了当前线程的各个地方都可以得到current的UOW的key
    /// 线程安全的Dictinoray容器是一个类实例，用于存放UOW的实例，通过UOW的key就可以取到UOW的实例。
    public class CallContextCurrentUnitOfWorkProvider : ICurrentUnitOfWorkProvider, ITransientDependency
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 上下文键
        /// </summary>
        private const string ContextKey = "Abp.UnitOfWork.Current";

        //TODO: Clear periodically..?
        //定期清理？
        private static readonly ConcurrentDictionary<string, IUnitOfWork> UnitOfWorkDictionary = new ConcurrentDictionary<string, IUnitOfWork>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public CallContextCurrentUnitOfWorkProvider()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 获取当前工作单元
        /// </summary>
        /// <param name="logger">日志</param>
        /// <returns></returns>
        private static IUnitOfWork GetCurrentUow(ILogger logger)
        {
            //获取当前工作单元key
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                return null;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                //如果根据key获取不到当前工作单元，那么就从当前线程集合（CallContext）中释放key
                //logger.Warn("There is a unitOfWorkKey in CallContext but not in UnitOfWorkDictionary (on GetCurrentUow)! UnitOfWork key: " + unitOfWorkKey);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            if (unitOfWork.IsDisposed)
            {
                //如果当前工作单元已经dispose，那么就从工作单元集合中移除，并将key从当前线程集合（CallContext）中释放
                logger.Warn("There is a unitOfWorkKey in CallContext but the UOW was disposed!");
                UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            return unitOfWork;
        }

        /// <summary>
        /// 设置当前工作单元
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="logger">日志</param>
        private static void SetCurrentUow(IUnitOfWork value, ILogger logger)
        {
            if (value == null)
            {
                //如果在set的时候设置为null，便表示要退出当前工作单元
                ExitFromCurrentUowScope(logger);
                return;
            }

            //获取当前工作单元的key
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey != null)
            {
                IUnitOfWork outer;
                if (UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out outer))
                {
                    if (outer == value)
                    {
                        logger.Warn("Setting the same UOW to the CallContext, no need to set again!");
                        return;
                    }

                    //到这里也就表示当前存在工作单元，那么再次设置工作单元，不是替换掉当前的工作单元而是将当前工作单元作为本次设置的工作单元的外层工作单元
                    value.Outer = outer;
                }
                else
                {
                    //logger.Warn("There is a unitOfWorkKey in CallContext but not in UnitOfWorkDictionary (on SetCurrentUow)! UnitOfWork key: " + unitOfWorkKey);
                }
            }

            unitOfWorkKey = value.Id;
            if (!UnitOfWorkDictionary.TryAdd(unitOfWorkKey, value))
            {
                //如果向工作单元中添加工作单元失败，便抛出异常
                throw new AbpException("Can not set unit of work! UnitOfWorkDictionary.TryAdd returns false!");
            }

            //设置当前线程的工作单元key
            CallContext.LogicalSetData(ContextKey, unitOfWorkKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        private static void ExitFromCurrentUowScope(ILogger logger)
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                logger.Warn("There is no current UOW to exit!");
                return;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                //logger.Warn("There is a unitOfWorkKey in CallContext but not in UnitOfWorkDictionary (on ExitFromCurrentUowScope)! UnitOfWork key: " + unitOfWorkKey);
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
            if (unitOfWork.Outer == null)
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            //Restore outer UOW

            var outerUnitOfWorkKey = unitOfWork.Outer.Id;
            if (!UnitOfWorkDictionary.TryGetValue(outerUnitOfWorkKey, out unitOfWork))
            {
                //No outer UOW
                logger.Warn("Outer UOW key could not found in UnitOfWorkDictionary!");
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            CallContext.LogicalSetData(ContextKey, outerUnitOfWorkKey);
        }

        /// <summary>
        /// 当前工作单元，
        /// DoNotWire是为了不让Ioc进行属性注入
        /// </summary>
        /// <inheritdoc />
        [DoNotWire]
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(Logger); }
            set { SetCurrentUow(value, Logger); }
        }
    }
}