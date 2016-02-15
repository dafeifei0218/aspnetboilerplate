using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Collections.Extensions;
using Castle.Core.Logging;

namespace Abp.Reflection
{
    /// <summary>
    /// 类型查找类
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 程序集查找
        /// </summary>
        public IAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TypeFinder()
        {
            AssemblyFinder = CurrentDomainAssemblyFinder.Instance;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 查找类型
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return GetAllTypes().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找全部类型
        /// </summary>
        /// <returns></returns>
        public Type[] FindAll()
        {
            return GetAllTypes().ToArray();
        }

        /// <summary>
        /// 获取全部类型
        /// </summary>
        /// <returns></returns>
        private List<Type> GetAllTypes()
        {
            var allTypes = new List<Type>();

            foreach (var assembly in AssemblyFinder.GetAllAssemblies().Distinct())
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            return allTypes;
        }
    }
}