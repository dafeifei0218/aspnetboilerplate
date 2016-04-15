using System;
using System.Collections.Generic;
using Abp.Dependency;
using Abp.Modules;

namespace Abp.MemoryDb
{
    /// <summary>
    /// 内存数据库
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]
    public class MemoryDatabase : ISingletonDependency
    {
        /// <summary>
        /// 设置字典
        /// </summary>
        private readonly Dictionary<Type, object> _sets;

        /// <summary>
        /// 同步对象
        /// </summary>
        private readonly object _syncObj = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoryDatabase()
        {
            _sets = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <returns></returns>
        public List<TEntity> Set<TEntity>()
        {
            var entityType = typeof(TEntity);

            lock (_syncObj)
            {
                if (!_sets.ContainsKey(entityType))
                {
                    _sets[entityType] = new List<TEntity>();
                }

                return _sets[entityType] as List<TEntity>;
            }
        }
    }
}