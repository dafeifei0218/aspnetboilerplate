using Abp.Localization;
using Abp.Modules;
using System.Reflection;
using Abp.Reflection;
using AutoMapper;
using Castle.Core.Logging;

namespace Abp.AutoMapper
{
    /// <summary>
    /// AbpAutoMapper模块
    /// </summary>
    [DependsOn(typeof (AbpKernelModule))]
    public class AbpAutoMapperModule : AbpModule
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        private readonly ITypeFinder _typeFinder;

        private static bool _createdMappingsBefore;
        private static readonly object _syncObj = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public AbpAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        public override void PostInitialize()
        {
            CreateMappings();
        }

        /// <summary>
        /// 创建映射
        /// </summary>
        private void CreateMappings()
        {
            lock (_syncObj)
            {
                //We should prevent duplicate mapping in an application, since AutoMapper is static.
                if (_createdMappingsBefore)
                {
                    return;
                }

                FindAndAutoMapTypes();
                CreateOtherMappings();

                _createdMappingsBefore = true;
            }
        }

        /// <summary>
        /// 查找AutoMap、AutoMapFrom、AutoMapTo自定义属性
        /// </summary>
        private void FindAndAutoMapTypes()
        {
            var types = _typeFinder.Find(type =>
                type.IsDefined(typeof(AutoMapAttribute)) ||
                type.IsDefined(typeof(AutoMapFromAttribute)) ||
                type.IsDefined(typeof(AutoMapToAttribute))
                );

            Logger.DebugFormat("Found {0} classes defines auto mapping attributes", types.Length);
            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                AutoMapperHelper.CreateMap(type);
            }
        }

        /// <summary>
        /// 创建其他映射
        /// </summary>
        private void CreateOtherMappings()
        {
            var localizationManager = IocManager.Resolve<ILocalizationManager>();
            Mapper.CreateMap<LocalizableString, string>().ConvertUsing(ls => localizationManager.GetString(ls));
        }
    }
}
