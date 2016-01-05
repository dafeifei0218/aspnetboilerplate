namespace Abp.Dependency
{
    /// <summary>
    /// һ�������������װ��
    /// </summary>
    internal class DisposableDependencyObjectWrapper : DisposableDependencyObjectWrapper<object>, IDisposableDependencyObjectWrapper
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver">IOC���Ʒ�ת������</param>
        /// <param name="obj">����</param>
        public DisposableDependencyObjectWrapper(IIocResolver iocResolver, object obj)
            : base(iocResolver, obj)
        {

        }
    }

    /// <summary>
    /// һ�������������װ��
    /// </summary>
    /// <typeparam name="T">����</typeparam>
    internal class DisposableDependencyObjectWrapper<T> : IDisposableDependencyObjectWrapper<T>
    {
        /// <summary>
        /// IOC���Ʒ�ת������
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// ����
        /// </summary>
        public T Object { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver">IOC���Ʒ�ת������</param>
        /// <param name="obj">����</param>
        public DisposableDependencyObjectWrapper(IIocResolver iocResolver, T obj)
        {
            _iocResolver = iocResolver;
            Object = obj;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Dispose()
        {
            _iocResolver.Release(Object);
        }
    }
}