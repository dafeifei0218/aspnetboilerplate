using System;
using System.Collections.Generic;
using Abp.Dependency;
using Hangfire;

namespace Abp.Hangfire
{
    /// <summary>
    /// Hangfire Ioc工作激活
    /// </summary>
    public class HangfireIocJobActivator : JobActivator
    {
        /// <summary>
        /// Ioc控制反转解析器
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">Ioc控制反转解析器</param>
        public HangfireIocJobActivator(IIocResolver iocResolver)
        {
            if (iocResolver == null)
            {
                throw new ArgumentNullException("iocResolver");
            }

            _iocResolver = iocResolver;
        }

        /// <summary>
        /// 激活工作
        /// </summary>
        /// <param name="jobType">工作类型</param>
        /// <returns></returns>
        public override object ActivateJob(Type jobType)
        {
            return _iocResolver.Resolve(jobType);
        }

        /// <summary>
        /// 开始范围
        /// </summary>
        /// <returns></returns>
        public override JobActivatorScope BeginScope()
        {
            return new HangfireIocJobActivatorScope(this, _iocResolver);
        }

        /// <summary>
        /// Hangfire Ioc工作激活范围
        /// </summary>
        class HangfireIocJobActivatorScope : JobActivatorScope
        {
            /// <summary>
            /// 工作激活。
            /// </summary>
            private readonly JobActivator _activator;
            /// <summary>
            /// IOC控制反转解析器。
            /// </summary>
            private readonly IIocResolver _iocResolver;

            /// <summary>
            /// 解析对象。
            /// </summary>
            private readonly List<object> _resolvedObjects;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="activator">工作激活。</param>
            /// <param name="iocResolver">IOC控制反转解析器。</param>
            public HangfireIocJobActivatorScope(JobActivator activator, IIocResolver iocResolver)
            {
                _activator = activator;
                _iocResolver = iocResolver;
                _resolvedObjects = new List<object>();
            }

            /// <summary>
            /// 解析
            /// </summary>
            /// <param name="type">类型</param>
            /// <returns></returns>
            public override object Resolve(Type type)
            {
                var instance = _activator.ActivateJob(type);
                _resolvedObjects.Add(instance);
                return instance;
            }

            /// <summary>
            /// 销毁范围
            /// </summary>
            public override void DisposeScope()
            {
                _resolvedObjects.ForEach(_iocResolver.Release);
            }
        }
    }
}