using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FluentNHibernate.Mapping;

namespace Abp.NHibernate.EntityMappings
{
    /// <summary>
    /// This class is used to make mapping easier for standart columns.
    /// Nh映射扩展类，使用来做映射的标准更容易。
    /// </summary>
    public static class NhMappingExtensions
    {
        /// <summary>
        /// Maps full audit columns (defined by <see cref="IFullAudited"/>).
        /// 映射全部设计列（定义<see cref="IFullAudited"/>）。
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapFullAudited<TEntity>(this ClassMap<TEntity> mapping)
            where TEntity : IFullAudited
        {
            mapping.MapAudited();
            mapping.MapDeletionAudited();
        }

        /// <summary>
        /// Maps audit columns. See <see cref="IAudited"/>.
        /// 映射审计列。看<see cref="IAudited"/>
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapAudited<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IAudited
        {
            mapping.MapCreationAudited();
            mapping.MapModificationAudited();
        }

        /// <summary>
        /// Maps creation audit columns. See <see cref="ICreationAudited"/>.
        /// 映射创建审计列。看<see cref="ICreationAudited"/>
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapCreationAudited<TEntity>(this ClassMap<TEntity> mapping) where TEntity : ICreationAudited
        {
            mapping.MapCreationTime();
            mapping.Map(x => x.CreatorUserId);
        }

        /// <summary>
        /// Maps CreationTime column. See <see cref="ICreationAudited"/>.
        /// 映射创建时间列。看<see cref="ICreationAudited"/>
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapCreationTime<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IHasCreationTime
        {
            mapping.Map(x => x.CreationTime);
        }

        /// <summary>
        /// Maps LastModificationTime column. See <see cref="IHasModificationTime"/>.
        /// 映射最后修改时间列。看<see cref="IHasModificationTime"/>
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapLastModificationTime<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IHasModificationTime
        {
            mapping.Map(x => x.LastModificationTime);
        }

        /// <summary>
        /// Maps modification audit columns. See <see cref="IModificationAudited"/>.
        /// 映射修改审计列。
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapModificationAudited<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IModificationAudited
        {
            mapping.MapLastModificationTime();
            mapping.Map(x => x.LastModifierUserId);
        }

        /// <summary>
        /// Maps deletion audit columns (defined by <see cref="IDeletionAudited"/>).
        /// 映射删除审计列（定义<see cref="IDeletionAudited"/>）
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapDeletionAudited<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IDeletionAudited
        {
            mapping.MapIsDeleted();
            mapping.Map(x => x.DeleterUserId);
            mapping.Map(x => x.DeletionTime);
        }

        /// <summary>
        /// Maps IsDeleted column (defined by <see cref="ISoftDelete"/>).
        /// 映射是否删除列（定义<see cref="ISoftDelete"/>）
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapIsDeleted<TEntity>(this ClassMap<TEntity> mapping) where TEntity : ISoftDelete
        {
            mapping.Map(x => x.IsDeleted);
        }

        /// <summary>
        /// Maps MapIsActive column (defined by <see cref="IPassivable"/>).
        /// 映射是否激活列（定义<see cref="IPassivable"/>）
        /// </summary>
        /// <typeparam name="TEntity">Entity type 实体类型</typeparam>
        public static void MapIsActive<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IPassivable
        {
            mapping.Map(x => x.IsActive);
        }
    }
}