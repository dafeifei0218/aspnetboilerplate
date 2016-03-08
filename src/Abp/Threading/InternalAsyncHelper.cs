using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Abp.Threading
{
    /// <summary>
    /// 内部异步帮助类
    /// </summary>
    internal static class InternalAsyncHelper
    {
        /// <summary>
        /// 等待任务完成，并在Finally快中执行Action
        /// </summary>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static async Task AwaitTaskWithFinally(Task actualReturnValue, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// 等待任务
        /// </summary>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="postAction">方法</param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
                await postAction();
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="preAction">预先执行的方法</param>
        /// <param name="postAction"></param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static async Task AwaitTaskWithPreActionAndPostActionAndFinally(Func<Task> actualReturnValue, Func<Task> preAction = null, Func<Task> postAction = null, Action<Exception> finalAction = null)
        {
            Exception exception = null;

            try
            {
                if (preAction != null)
                {
                    await preAction();
                }

                await actualReturnValue();

                if (postAction != null)
                {
                    await postAction();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                if (finalAction != null)
                {
                    finalAction(exception);                    
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static async Task<T> AwaitTaskWithFinallyAndGetResult<T>(Task<T> actualReturnValue, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                return await actualReturnValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskReturnType"></param>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static object CallAwaitTaskWithFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Action<Exception> finalAction)
        {
            return typeof(InternalAsyncHelper)
                .GetMethod("AwaitTaskWithFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, finalAction });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="postAction"></param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static async Task<T> AwaitTaskWithPostActionAndFinallyAndGetResult<T>(Task<T> actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                var result = await actualReturnValue;
                await postAction();
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskReturnType"></param>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="action"></param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static object CallAwaitTaskWithPostActionAndFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Func<Task> action, Action<Exception> finalAction)
        {
            return typeof (InternalAsyncHelper)
                .GetMethod("AwaitTaskWithPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, action, finalAction });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="preAction">预先执行的方法</param>
        /// <param name="postAction"></param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static async Task<T> AwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult<T>(Func<Task<T>> actualReturnValue, Func<Task> preAction = null, Func<Task> postAction = null, Action<Exception> finalAction = null)
        {
            Exception exception = null;

            try
            {
                if (preAction != null)
                {
                    await preAction();
                }

                var result = await actualReturnValue();

                if (postAction != null)
                {
                    await postAction();                    
                }

                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                if (finalAction != null)
                {
                    finalAction(exception);
                }
            }
        }

        /// <summary>
        /// 调用AwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult
        /// </summary>
        /// <param name="taskReturnType"></param>
        /// <param name="actualReturnValue">实际返回值</param>
        /// <param name="preAction">预先执行的方法</param>
        /// <param name="postAction"></param>
        /// <param name="finalAction">最终执行的方法</param>
        /// <returns></returns>
        public static object CallAwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult(Type taskReturnType, Func<object> actualReturnValue, Func<Task> preAction = null, Func<Task> postAction = null, Action<Exception> finalAction = null)
        {
            return typeof(InternalAsyncHelper)
                .GetMethod("AwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, preAction, postAction, finalAction });
        }
    }
}