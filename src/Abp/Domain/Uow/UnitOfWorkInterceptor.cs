using System.Threading.Tasks;
using Abp.Threading;
using Castle.DynamicProxy;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This interceptor is used to manage database connection and transactions.
    /// 工作单元拦截器
    /// </summary>
    internal class UnitOfWorkInterceptor : IInterceptor
    {
        /// <summary>
        /// 工作单元管理类
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWorkManager">工作单元管理类</param>
        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Intercepts a method.
        /// 拦截方法
        /// </summary>
        /// <param name="invocation">Method invocation arguments </param>
        public void Intercept(IInvocation invocation)
        {
            if (_unitOfWorkManager.Current != null)
            {
                //如果当前已经在工作单元中，则直接执行被拦截类的方法
                //Continue with current uow
                invocation.Proceed();
                return;
            }

            //获取方法上的UnitOfWorkAttribute，如果没有返回NULL，invocation.MethodInvocationTarget为被拦截类的类型
            var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(invocation.MethodInvocationTarget);
            if (unitOfWorkAttr == null || unitOfWorkAttr.IsDisabled)
            {
                //如果当前方法上没有UnitOfWorkAttribute或者是设置为Disabled，则直接调用呗拦截类的方法
                //No need to a uow
                invocation.Proceed();
                return;
            }

            //表示是需要将这个方法作为工作单元，详情点击查看
            //No current uow, run a new one
            PerformUow(invocation, unitOfWorkAttr.CreateOptions());
        }

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <param name="options">工作单元选项</param>
        private void PerformUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncUow(invocation, options);
            }
            else
            {
                PerformSyncUow(invocation, options);
            }
        }

        /// <summary>
        /// 同步执行工作单元
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <param name="options">工作单元选项</param>
        /// UnitOfWorkInterceptor拦截器调用调用UnitOfWorkManager开启UOW流程的
        private void PerformSyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            using (var uow = _unitOfWorkManager.Begin(options))
            {
                invocation.Proceed();
                uow.Complete();
            }
        }

        /// <summary>
        /// 异步执行工作单元
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <param name="options">工作单元选项</param>
        private void PerformAsyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            var uow = _unitOfWorkManager.Begin(options);

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                    (Task)invocation.ReturnValue,
                    async () => await uow.CompleteAsync(),
                    exception => uow.Dispose()
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    async () => await uow.CompleteAsync(),
                    (exception) => uow.Dispose()
                    );
            }
        }
    }
}