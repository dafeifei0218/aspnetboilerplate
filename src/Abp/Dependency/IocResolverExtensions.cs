using System;

namespace Abp.Dependency
{
    /// <summary>
    /// Extension methods to <see cref="IIocResolver"/> interface.
    /// IocResolver解析扩展类
    /// </summary>
    public static class IocResolverExtensions
    {
        #region ResolveAsDisposable

        /// <summary>
        /// Gets an <see cref="DisposableDependencyObjectWrapper{T}"/> object that wraps resolved object to be Disposable.
        /// 一次性解析，获取一个DisposableDependencyObjectWrapper，一次性的包装解决对象。
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 获取对象的类型</typeparam>
        /// <param name="iocResolver">IIocResolver object IOC解析对象</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/> 包装实例对象</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>());
        }

        /// <summary>
        /// Gets an <see cref="DisposableDependencyObjectWrapper{T}"/> object that wraps resolved object to be Disposable.
        /// 一次性解析
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 获取对象的类型</typeparam>
        /// <param name="iocResolver">IIocResolver object IOC解析对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible <see cref="T"/>. 对象类型解析</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/> 包装实例对象</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type));
        }

        /// <summary>
        /// Gets an <see cref="DisposableDependencyObjectWrapper{T}"/> object that wraps resolved object to be Disposable.
        /// 一次性解析
        /// </summary> 
        /// <param name="iocResolver">IIocResolver object IOC解析对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible to <see cref="IDisposable"/>. 对象类型解析</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/> 包装实例对象</returns>
        public static IDisposableDependencyObjectWrapper ResolveAsDisposable(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper(iocResolver, iocResolver.Resolve(type));
        }

        /// <summary>
        /// Gets an <see cref="DisposableDependencyObjectWrapper{T}"/> object that wraps resolved object to be Disposable.
        /// 一次性解析
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 获取对象的类型</typeparam>
        /// <param name="iocResolver">IIocResolver object IOC解析对象</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments 构造函数参数</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/> 包装实例对象</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>(argumentsAsAnonymousType));
        }

        /// <summary>
        /// Gets an <see cref="DisposableDependencyObjectWrapper{T}"/> object that wraps resolved object to be Disposable.
        /// 一次性解析
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 获取对象的类型</typeparam>
        /// <param name="iocResolver">IIocResolver object IOC解析对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible <see cref="T"/>. 对象类型解析</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments 构造函数参数</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/> 包装实例对象</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type, argumentsAsAnonymousType));
        }

        /// <summary>
        /// Gets an <see cref="DisposableDependencyObjectWrapper{T}"/> object that wraps resolved object to be Disposable.
        /// 一次性解析
        /// </summary> 
        /// <param name="iocResolver">IIocResolver object IOC解析对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible to <see cref="IDisposable"/>. 对象类型解析</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments 构造函数参数</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/> 包装实例对象</returns>
        public static IDisposableDependencyObjectWrapper ResolveAsDisposable(this IIocResolver iocResolver, Type type, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper(iocResolver, iocResolver.Resolve(type, argumentsAsAnonymousType));
        }

        #endregion

        #region Using

        /// <summary>
        /// This method can be used to resolve and release an object automatically.
        /// You can use the object in <see cref="action"/>. 
        /// 这个方法可以用来解析和释放一个自动对象。
        /// 您可以使用对象在action
        /// </summary> 
        /// <typeparam name="T">Type of the object to use 获取对象的类型</typeparam>
        /// <param name="iocResolver">IIocResolver object IOC解析对象</param>
        /// <param name="action">An action that can use the resolved object 一个可以使用该解析对象的操作</param>
        public static void Using<T>(this IIocResolver iocResolver, Action<T> action)
        {
            using (var wrapper = new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>()))
            {
                action(wrapper.Object);
            }
        }

        #endregion
    }
}
