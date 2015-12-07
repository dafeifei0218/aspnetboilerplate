﻿using System.Collections.Generic;
using Abp.Dependency;
using Abp.Reflection;
using Castle.DynamicProxy;

namespace Abp.Application.Features
{
    /// <summary>
    /// Intercepts methods to apply <see cref="RequiresFeatureAttribute"/>.
    /// 功能拦截器
    /// </summary>
    public class FeatureInterceptor : IInterceptor
    {
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureInterceptor"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">The ioc resolver. 依赖注入解析器</param>
        public FeatureInterceptor(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        /// <summary>
        /// 拦截器
        /// </summary>
        /// <param name="invocation">调用</param>
        public void Intercept(IInvocation invocation)
        {
            var featureAttributes =
                ReflectionHelper.GetAttributesOfMemberAndDeclaringType<RequiresFeatureAttribute>(
                    invocation.MethodInvocationTarget
                    );

            if (featureAttributes.Count <= 0)
            {
                invocation.Proceed();
                return;
            }

            CheckFeatures(featureAttributes);

            invocation.Proceed();
        }

        /// <summary>
        /// 检查功能
        /// </summary>
        /// <param name="featureAttributes">功能自定义属性</param>
        private void CheckFeatures(IEnumerable<RequiresFeatureAttribute> featureAttributes)
        {
            _iocResolver.Using<IFeatureChecker>(featureChecker =>
            {
                foreach (var featureAttribute in featureAttributes)
                {
                    featureChecker.CheckEnabled(featureAttribute.RequiresAll, featureAttribute.Features);
                }
            });
        }
    }
}
