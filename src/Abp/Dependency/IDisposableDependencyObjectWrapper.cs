using System;

namespace Abp.Dependency
{
    /// <summary>
    /// This interface is used to wrap an object that is resolved from IOC container.
    /// It inherits <see cref="IDisposable"/>, so resolved object can be easily released.
    /// In <see cref="IDisposable.Dispose"/> method, <see cref="IIocResolver.Release"/> is called to dispose the object.
    /// This is non-generic version of <see cref="IDisposableDependencyObjectWrapper{T}"/> interface.
    /// 一次性依赖对象包装器接口
    /// </summary>
    public interface IDisposableDependencyObjectWrapper : IDisposableDependencyObjectWrapper<object>
    {

    }
}