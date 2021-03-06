﻿using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;
using Abp.Dependency;
using Abp.IO.Extensions;

namespace Abp.Resources.Embedded
{
    /// <summary>
    /// 嵌入资源管理类
    /// </summary>
    public class EmbeddedResourceManager : IEmbeddedResourceManager, ISingletonDependency
    {
        private readonly ConcurrentDictionary<string, EmbeddedResourcePathInfo> _resourcePaths; //Key: Root path of the resource
        private readonly ConcurrentDictionary<string, EmbeddedResourceInfo> _resourceCache; //Key: Root path of the resource

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public EmbeddedResourceManager()
        {
            _resourcePaths = new ConcurrentDictionary<string, EmbeddedResourcePathInfo>();
            _resourceCache = new ConcurrentDictionary<string, EmbeddedResourceInfo>();
        }

        /// <summary>
        /// 暴露资源
        /// </summary>
        /// <inheritdoc/>
        public void ExposeResources(string rootPath, Assembly assembly, string resourceNamespace)
        {
            if (_resourcePaths.ContainsKey(rootPath))
            {
                throw new AbpException("There is already an embedded resource with given rootPath: " + rootPath);
            }

            _resourcePaths[rootPath] = new EmbeddedResourcePathInfo(rootPath, assembly, resourceNamespace);
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="fullPath">全路径</param>
        /// <inheritdoc/>
        public EmbeddedResourceInfo GetResource(string fullPath)
        {
            //Get from cache if exists!
            //如果缓存中有，从缓存中获得
            if (_resourceCache.ContainsKey(fullPath))
            {
                return _resourceCache[fullPath];
            }

            var pathInfo = GetPathInfoForFullPath(fullPath);

            using (var stream = pathInfo.Assembly.GetManifestResourceStream(pathInfo.FindManifestName(fullPath)))
            {
                if (stream == null)
                {
                    throw new AbpException("There is no exposed embedded resource for " + fullPath);
                }

                return _resourceCache[fullPath] = new EmbeddedResourceInfo(stream.GetAllBytes(), pathInfo.Assembly);
            }
        }

        /// <summary>
        /// 从全路径获取路径信息
        /// </summary>
        /// <param name="fullPath">全路径</param>
        private EmbeddedResourcePathInfo GetPathInfoForFullPath(string fullPath)
        {
            foreach (var resourcePathInfo in _resourcePaths.Values.ToImmutableList()) //TODO@hikalkan: Test for multi-threading (possible multiple enumeration problem).
            {
                if (fullPath.StartsWith(resourcePathInfo.Path))
                {
                    return resourcePathInfo;
                }
            }

            throw new AbpException("There is no exposed embedded resource for: " + fullPath);
        }
    }
}