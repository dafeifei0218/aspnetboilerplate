using System;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// Add this class to a DbContext to define auto-repository types for entities in this DbContext.
    /// This is useful if you inherit same DbContext by more than one DbContext.
    /// Abp仓储类型自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        /// <summary>
        /// 默认仓储类型
        /// </summary>
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        /// <summary>
        /// 仓储接口类型
        /// </summary>
        public Type RepositoryInterface { get; private set; }

        /// <summary>
        /// 有主键的仓储接口类型
        /// </summary>
        public Type RepositoryInterfaceWithPrimaryKey { get; private set; }

        /// <summary>
        /// 仓储实现类型
        /// </summary>
        public Type RepositoryImplementation { get; private set; }

        /// <summary>
        /// 有主键的仓储实现类型
        /// </summary>
        public Type RepositoryImplementationWithPrimaryKey { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        static AutoRepositoryTypesAttribute()
        {
            Default = new AutoRepositoryTypesAttribute(
                typeof (IRepository<>),
                typeof (IRepository<,>),
                typeof (EfRepositoryBase<,>),
                typeof (EfRepositoryBase<,,>)
                );
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryInterface">仓储接口类型</param>
        /// <param name="repositoryInterfaceWithPrimaryKey">有主键的仓储接口类型</param>
        /// <param name="repositoryImplementation">仓储实现类型</param>
        /// <param name="repositoryImplementationWithPrimaryKey">有主键的仓储实现类型</param>
        public AutoRepositoryTypesAttribute(
            Type repositoryInterface, 
            Type repositoryInterfaceWithPrimaryKey, 
            Type repositoryImplementation, 
            Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}