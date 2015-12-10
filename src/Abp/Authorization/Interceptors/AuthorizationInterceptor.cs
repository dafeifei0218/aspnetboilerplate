using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Reflection;
using Abp.Threading;
using Castle.DynamicProxy;

namespace Abp.Authorization.Interceptors
{
    /// <summary>
    /// This class is used to intercept methods to make authorization if the method defined <see cref="AbpAuthorizeAttribute"/>.
    /// 授权拦截器，如果方法定义AbpAuthorizeAttribute，使用此类来拦截方法进行授权
    /// </summary>
    public class AuthorizationInterceptor : IInterceptor
    {
        //IOC控制反转解析器
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">IOC控制反转解析器</param>
        public AuthorizationInterceptor(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="invocation">调用</param>
        public void Intercept(IInvocation invocation)
        {
            //获取方法的AbpAuthorizeAttribute授权自定义属性
            var authorizeAttributes =
                ReflectionHelper.GetAttributesOfMemberAndDeclaringType<AbpAuthorizeAttribute>(
                    invocation.MethodInvocationTarget
                    );

            //如果没有授权自定义属性
            if (authorizeAttributes.Count <= 0)
            {
                invocation.Proceed();
                return;
            }

            //异步操作前不使用Castle Windsor.
            //TODO: Async pre-action does not works with Castle Windsor. So, it's cancelled until another solution is found (issue #381).

            //if (AsyncHelper.IsAsyncMethod(invocation.Method))
            //{
            //    InterceptAsync(invocation, authorizeAttributes);
            //}
            //else
            //{
                InterceptSync(invocation, authorizeAttributes);
            //}
        }

        /// <summary>
        /// 拦截-异步
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <param name="authorizeAttributes">AbpAuthorizeAttribute授权自定义属性集合</param>
        private void InterceptAsync(IInvocation invocation, IEnumerable<AbpAuthorizeAttribute> authorizeAttributes)
        {
            //方法的返回类型为异步
            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper
                    .AwaitTaskWithPreActionAndPostActionAndFinally(
                        () =>
                        {
                            invocation.Proceed();
                            return (Task)invocation.ReturnValue;
                        },
                        preAction: () => AuthorizeAsync(authorizeAttributes)
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper
                    .CallAwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult(
                        invocation.Method.ReturnType.GenericTypeArguments[0],
                        () =>
                        {
                            invocation.Proceed();
                            return invocation.ReturnValue;
                        },
                        preAction: async () => await AuthorizeAsync(authorizeAttributes)
                    );
            }
        }

        /// <summary>
        /// 拦截-同步
        /// </summary>
        /// <param name="invocation">调用</param>
        /// <param name="authorizeAttributes">AbpAuthorizeAttribute授权自定义属性集合</param>
        private void InterceptSync(IInvocation invocation, IEnumerable<AbpAuthorizeAttribute> authorizeAttributes)
        {
            Authorize(authorizeAttributes);
            invocation.Proceed();
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="authorizeAttributes">AbpAuthorizeAttribute授权自定义属性集合</param>
        private void Authorize(IEnumerable<AbpAuthorizeAttribute> authorizeAttributes)
        {
            using (var authorizationAttributeHelper = _iocResolver.ResolveAsDisposable<IAuthorizeAttributeHelper>())
            {
                authorizationAttributeHelper.Object.Authorize(authorizeAttributes);
            }
        }

        /// <summary>
        /// 授权-异步
        /// </summary>
        /// <param name="authorizeAttributes">AbpAuthorizeAttribute授权自定义属性集合</param>
        /// <returns></returns>
        private async Task AuthorizeAsync(IEnumerable<AbpAuthorizeAttribute> authorizeAttributes)
        {
            using (var authorizationAttributeHelper = _iocResolver.ResolveAsDisposable<IAuthorizeAttributeHelper>())
            {
                await authorizationAttributeHelper.Object.AuthorizeAsync(authorizeAttributes);
            }
        }
    }
}
