namespace Abp.Dependency
{
    /// <summary>
    /// 一次性依赖对象包装器
    /// </summary>
    internal class DisposableDependencyObjectWrapper : DisposableDependencyObjectWrapper<object>, IDisposableDependencyObjectWrapper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">IOC控制反转解析器</param>
        /// <param name="obj">对象</param>
        public DisposableDependencyObjectWrapper(IIocResolver iocResolver, object obj)
            : base(iocResolver, obj)
        {

        }
    }

    /// <summary>
    /// 一次性依赖对象包装器
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    internal class DisposableDependencyObjectWrapper<T> : IDisposableDependencyObjectWrapper<T>
    {
        /// <summary>
        /// IOC控制反转解析器
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// 对象
        /// </summary>
        public T Object { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">IOC控制反转解析器</param>
        /// <param name="obj">对象</param>
        public DisposableDependencyObjectWrapper(IIocResolver iocResolver, T obj)
        {
            _iocResolver = iocResolver;
            Object = obj;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            _iocResolver.Release(Object);
        }
    }
}