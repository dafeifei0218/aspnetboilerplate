using System;

namespace Abp.Threading.Extensions
{
    /// <summary>
    /// Extension methods to make locking easier.
    /// 锁扩展方法
    /// </summary>
    public static class LockExtensions
    {
        /// <summary>
        /// Executes given <see cref="action"/> by locking given <see cref="source"/> object.
        /// 锁定给的源对象，执行给定的动作
        /// </summary>
        /// <param name="source">Source object (to be locked) 源对象（被锁定）</param>
        /// <param name="action">Action (to be executed) 动作（执行）</param>
        public static void Locking(this object source, Action action)
        {
            lock (source)
            {
                action();
            }
        }

        /// <summary>
        /// Executes given <see cref="action"/> by locking given <see cref="source"/> object.
        /// 锁定给的源对象，执行给定的动作
        /// </summary>
        /// <typeparam name="T">Type of the object (to be locked) 对象的类型（被锁定）</typeparam>
        /// <param name="source">Source object (to be locked) 源对象（被锁定）</param>
        /// <param name="action">Action (to be executed) 动作（执行）</param>
        public static void Locking<T>(this T source, Action<T> action) where T : class
        {
            lock (source)
            {
                action(source);
            }
        }

        /// <summary>
        /// Executes given <see cref="func"/> and returns it's value by locking given <see cref="source"/> object.
        /// 锁定给的源对象，执行给定的委托和返回值
        /// </summary>
        /// <typeparam name="T">Type of the object (to be locked) 对象的类型（被锁定）</typeparam>
        /// <typeparam name="TResult">Return type 返回类型</typeparam>
        /// <param name="source">Source object (to be locked) 源对象（被锁定）</param>
        /// <param name="func">Function (to be executed) 委托</param>
        /// <returns>Return value of the <see cref="func"/> 委托的返回值</returns>
        public static TResult Locking<TResult>(this object source, Func<TResult> func)
        {
            lock (source)
            {
                return func();
            }
        }

        /// <summary>
        /// Executes given <see cref="func"/> and returns it's value by locking given <see cref="source"/> object.
        /// 锁定给的源对象，执行给定的委托和返回值
        /// </summary>
        /// <typeparam name="T">Type of the object (to be locked) 对象的类型（被锁定）</typeparam>
        /// <typeparam name="TResult">Return type 返回类型</typeparam>
        /// <param name="source">Source object (to be locked) 源对象（被锁定）</param>
        /// <param name="func">Function (to be executed) 委托</param>
        /// <returns>Return value of the <see cref="func"/> 委托的返回值</returns>
        public static TResult Locking<T, TResult>(this T source, Func<T, TResult> func) where T : class
        {
            lock (source)
            {
                return func(source);
            }
        }
    }
}
