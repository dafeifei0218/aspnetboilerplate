using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Castle.Core.Logging;

namespace Abp.Modules
{
    /// <summary>
    /// This class is used to manage modules.
    /// Abp模块管理类
    /// </summary>
    internal class AbpModuleManager : IAbpModuleManager
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        private readonly AbpModuleCollection _modules;

        private readonly IIocManager _iocManager;
        private readonly IModuleFinder _moduleFinder;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocManager">IOC管理类</param>
        /// <param name="moduleFinder">模块查找器</param>
        public AbpModuleManager(IIocManager iocManager, IModuleFinder moduleFinder)
        {
            _modules = new AbpModuleCollection();
            _iocManager = iocManager;
            _moduleFinder = moduleFinder;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 初始化模块
        /// </summary>
        public virtual void InitializeModules()
        {
            LoadAll();

            //根据依赖关系，获取排序后的模块
            var sortedModules = _modules.GetSortedModuleListByDependency();

            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        /// <summary>
        /// 关闭模块
        /// </summary>
        public virtual void ShutdownModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());
        }

        /// <summary>
        /// 加载全部
        /// </summary>
        private void LoadAll()
        {
            Logger.Debug("Loading Abp modules...");

            var moduleTypes = AddMissingDependedModules(_moduleFinder.FindAll());
            Logger.Debug("Found " + moduleTypes.Count + " ABP modules in total.");

            //Register to IOC container.
            //注册IOC容器
            foreach (var moduleType in moduleTypes)
            {
                if (!AbpModule.IsAbpModule(moduleType))
                {
                    throw new AbpInitializationException("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
                }

                if (!_iocManager.IsRegistered(moduleType))
                {
                    _iocManager.Register(moduleType);
                }
            }

            //Add to module collection
            //添加到模块集合
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = (AbpModule)_iocManager.Resolve(moduleType);

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IAbpStartupConfiguration>();

                _modules.Add(new AbpModuleInfo(moduleObject));

                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }

            EnsureKernelModuleToBeFirst();

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        /// <summary>
        /// 确保核心模块在第一个
        /// </summary>
        private void EnsureKernelModuleToBeFirst()
        {
            var kernelModuleIndex = _modules.FindIndex(m => m.Type == typeof (AbpKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = _modules[kernelModuleIndex];
                _modules.RemoveAt(kernelModuleIndex);
                _modules.Insert(0, kernelModule);
            }
        }

        /// <summary>
        /// 设置依赖
        /// </summary>
        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                //Set dependencies according to assembly dependency
                //根据程序集依赖设置依赖关系
                foreach (var referencedAssemblyName in moduleInfo.Assembly.GetReferencedAssemblies())
                {
                    var referencedAssembly = Assembly.Load(referencedAssemblyName);
                    var dependedModuleList = _modules.Where(m => m.Assembly == referencedAssembly).ToList();
                    if (dependedModuleList.Count > 0)
                    {
                        moduleInfo.Dependencies.AddRange(dependedModuleList);
                    }
                }

                //Set dependencies for defined DependsOnAttribute attribute(s).
                //定义DependsOnAttribute属性，设置依赖
                foreach (var dependedModuleType in AbpModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new AbpInitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }

        /// <summary>
        /// 添加缺失的模块
        /// </summary>
        /// <param name="allModules">全部模块</param>
        /// <returns></returns>
        private static ICollection<Type> AddMissingDependedModules(ICollection<Type> allModules)
        {
            var initialModules = allModules.ToList();
            foreach (var module in initialModules)
            {
                FillDependedModules(module, allModules);
            }

            return allModules;
        }

        /// <summary>
        /// 查找依赖模块
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="allModules">全部模块</param>
        private static void FillDependedModules(Type module, ICollection<Type> allModules)
        {
            foreach (var dependedModule in AbpModule.FindDependedModuleTypes(module))
            {
                if (!allModules.Contains(dependedModule))
                {
                    allModules.Add(dependedModule);
                    FillDependedModules(dependedModule, allModules);
                }
            }
        }
    }
}
