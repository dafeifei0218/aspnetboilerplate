using Castle.DynamicProxy;

namespace Abp.Runtime.Validation.Interception
{
    /// <summary>
    /// This interceptor is used intercept method calls for classes which's methods must be validated.
    /// 验证拦截器，
    /// 这个拦截器是用来拦截方法的，必须验证方法的类
    /// </summary>
    /// <remarks>
    /// 上面MethodInvocationValidator的Validate方法是由ValidationInterceptor触发的。这是一个自定义的Castle拦截器。
    /// </remarks>
    public class ValidationInterceptor : IInterceptor
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="invocation">调用</param>
        public void Intercept(IInvocation invocation)
        {
            new MethodInvocationValidator(
                invocation.Method,
                invocation.Arguments
                ).Validate();

            invocation.Proceed();
        }
    }
}
