using System;

namespace Abp.Dependency
{
    /// <summary>
    /// Used to get a singleton of any class which can be resolved using <see cref="IocManager.Instance"/>.
    /// Important: Use classes by injecting wherever possible. This class is for cases that's not possible.
    /// 单例依赖
    /// 任何一个使用了单例的类，可以用<see cref="IocManager.Instance"/>解析。
    /// 重要：尽可能地使用类。这个类是不可能的。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonDependency<T>
    {
        /// <summary>
        /// Gets the instance.
        /// 实例，获取实例
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static T Instance { get { return LazyInstance.Value; } }
        /// <summary>
        /// 懒加载的实例
        /// </summary>
        private static readonly Lazy<T> LazyInstance;

        /// <summary>
        /// 构造函数
        /// </summary>
        static SingletonDependency()
        {
            LazyInstance = new Lazy<T>(() => IocManager.Instance.Resolve<T>());
        }
    }
}
