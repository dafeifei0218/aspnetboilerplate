using System;

namespace Abp.Dependency
{
    /// <summary>
    /// This interface is used to wrap an object that is resolved from IOC container.
    /// It inherits <see cref="IDisposable"/>, so resolved object can be easily released.
    /// In <see cref="IDisposable.Dispose"/> method, <see cref="IIocResolver.Release"/> is called to dispose the object.
    /// 一次性依赖对象包装器接口，
    /// 这个几口是用来包装一个对象，这是IOC容器的解析。
    /// 它继承类IDisposabl，所以解析对象可以很容易的释放。
    /// 在IDisposable.Dispose方法，IIocResolver.Release是处理释放对象。
    /// </summary>
    /// <typeparam name="T">Type of the object 对象类型</typeparam>
    public interface IDisposableDependencyObjectWrapper<out T> : IDisposable
    {
        /// <summary>
        /// The resolved object.
        /// 解析对象
        /// </summary>
        T Object { get; }
    }
}