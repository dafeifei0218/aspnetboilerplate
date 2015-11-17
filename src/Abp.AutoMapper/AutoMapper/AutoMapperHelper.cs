using System;
using System.Reflection;
using Abp.Collections.Extensions;
using AutoMapper;

namespace Abp.AutoMapper
{
    /// <summary>
    /// AutoMapper帮助类
    /// </summary>
    internal static class AutoMapperHelper
    {
        /// <summary>
        /// 创建映射
        /// </summary>
        /// <param name="type">类型</param>
        public static void CreateMap(Type type)
        {
            CreateMap<AutoMapFromAttribute>(type);
            CreateMap<AutoMapToAttribute>(type);
            CreateMap<AutoMapAttribute>(type);
        }

        /// <summary>
        /// 创建映射
        /// </summary>
        /// <typeparam name="TAttribute">AutoMap自定义属性或子属性</typeparam>
        /// <param name="type">类型</param>
        public static void CreateMap<TAttribute>(Type type)
            where TAttribute : AutoMapAttribute
        {
            //如果Type的类型不为AutoMapAttribute自定义属性或子属性，则返回
            if (!type.IsDefined(typeof (TAttribute)))
            {
                return;
            }

            //获取type的类型为AutoMapAttribute自定义属性
            foreach (var autoMapToAttribute in type.GetCustomAttributes<TAttribute>())
            {
                //如果类型的TargetTypes目标类型为空，则跳过
                if (autoMapToAttribute.TargetTypes.IsNullOrEmpty())
                {
                    continue;
                }

                foreach (var targetType in autoMapToAttribute.TargetTypes)
                {
                    //如果AutoMap的方向为To
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                    {
                        Mapper.CreateMap(type, targetType);
                    }

                    //如果AutoMap的方向为From
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                    {
                        Mapper.CreateMap(targetType, type);                                
                    }
                }
            }
        }
    }
}