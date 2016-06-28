using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.Json;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Timing;
using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace Abp.Auditing
{
    /// <summary>
    /// 审计拦截器
    /// </summary>
    /// <remarks>
    /// 满足以下四个条件的方法都会被AuditingInterceptor拦截：
    /// 1.IApplicationService的实例中的方法
    /// 2.添加了AuditedAttribute的类的实例的方法
    /// 3.加了AuditedAttribute的方法
    /// 4.通过IAuditingConfiguration对象的Selectors属性添加需要被auditing的类型。
    /// 
    /// 生成AuditInfo实例，然后调用IAuditingStore类实例执行AuditInfo持久化。
    /// </remarks>
    internal class AuditingInterceptor : IInterceptor
    {
        /// <summary>
        /// Abp会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 审计存储
        /// </summary>
        public IAuditingStore AuditingStore { get; set; }

        /// <summary>
        /// 审计配置
        /// </summary>
        private readonly IAuditingConfiguration _configuration;

        /// <summary>
        /// 审计信息提供者
        /// </summary>
        private readonly IAuditInfoProvider _auditInfoProvider;

        /// <summary>
        /// 工作单元管理类
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">审计配置</param>
        /// <param name="auditInfoProvider">审计信息提供者</param>
        /// <param name="unitOfWorkManager">工作单元管理类</param>
        public AuditingInterceptor(IAuditingConfiguration configuration, IAuditInfoProvider auditInfoProvider, IUnitOfWorkManager unitOfWorkManager)
        {
            _configuration = configuration;
            _auditInfoProvider = auditInfoProvider;
            _unitOfWorkManager = unitOfWorkManager;

            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            AuditingStore = SimpleLogAuditingStore.Instance;
        }

        /// <summary>
        /// 拦截器
        /// </summary>
        /// <param name="invocation">调用</param>
        public void Intercept(IInvocation invocation)
        {
            if (!AuditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget, _configuration, AbpSession))
            {
                invocation.Proceed();
                return;
            }

            //创建审计
            var auditInfo = CreateAuditInfo(invocation);

            //如果方法为异步方法
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                //执行异步审计
                PerformAsyncAuditing(invocation, auditInfo);
            }
            else
            {
                //执行同步审计
                PerformSyncAuditing(invocation, auditInfo);
            }
        }

        /// <summary>
        /// 创建审计信息
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <returns></returns>
        private AuditInfo CreateAuditInfo(IInvocation invocation)
        {
            var auditInfo = new AuditInfo
            {
                TenantId = AbpSession.TenantId,
                UserId = AbpSession.UserId,
                ImpersonatorUserId = AbpSession.ImpersonatorUserId,
                ImpersonatorTenantId = AbpSession.ImpersonatorTenantId,
                ServiceName = invocation.MethodInvocationTarget.DeclaringType != null
                    ? invocation.MethodInvocationTarget.DeclaringType.FullName
                    : "",
                MethodName = invocation.MethodInvocationTarget.Name,
                Parameters = ConvertArgumentsToJson(invocation),
                ExecutionTime = Clock.Now
            };

            _auditInfoProvider.Fill(auditInfo);

            return auditInfo;
        }

        /// <summary>
        /// 执行同步审计
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <param name="auditInfo">审计信息</param>
        private void PerformSyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                AuditingStore.Save(auditInfo);
            }
        }

        /// <summary>
        /// 执行异步审计
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <param name="auditInfo">审计信息</param>
        private void PerformAsyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task) invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
        }

        /// <summary>
        /// 转换参数到JSON
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <returns></returns>
        private string ConvertArgumentsToJson(IInvocation invocation)
        {
            try
            {
                var parameters = invocation.MethodInvocationTarget.GetParameters();
                if (parameters.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = invocation.Arguments[i];
                    dictionary[parameter.Name] = argument;
                }

                return dictionary.ToJsonString(true);
            }
            catch (Exception ex)
            {
                Logger.Warn("Could not serialize arguments for method: " + invocation.MethodInvocationTarget.Name);
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        /// <summary>
        /// 保存审计信息
        /// </summary>
        /// <param name="auditInfo">审计信息</param>
        /// <param name="stopwatch">提供一组方法和属性，可用于准确地测量运行时间。</param>
        /// <param name="exception">异常</param>
        private void SaveAuditInfo(AuditInfo auditInfo, Stopwatch stopwatch, Exception exception)
        {
            stopwatch.Stop();
            auditInfo.Exception = exception;
            auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                AuditingStore.Save(auditInfo);
                uow.Complete();
            }
        }
    }
}