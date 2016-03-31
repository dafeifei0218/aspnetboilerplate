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

        /// <summary>
        /// 类型查找
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 创建映射之前判断是否已经创建
        /// </summary>
        private static bool _createdMappingsBefore;
        /// <summary>
        /// 同步对象
        /// </summary>
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

        /// <summary>
        /// 初始化之后执行
        /// </summary>
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
                //我们应该防止应用程序重复映射，由于AutoMapper是静态的。
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

            //找到几个类，定义的自动映射属性
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
