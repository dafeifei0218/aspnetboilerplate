﻿using System;

namespace Abp.Dependency
{
    /// <summary>
    /// Used to get a singleton of any class which can be resolved using <see cref="IocManager.Instance"/>.
    /// Important: Use classes by injecting wherever possible. This class is for cases that's not possible.
    /// 单例依赖
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonDependency<T>
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static T Instance { get { return LazyInstance.Value; } }
        /// <summary>
        /// 
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
