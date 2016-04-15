using Abp.Domain.Uow;
using FluentNHibernate.Mapping;
using NHibernate;

namespace Abp.NHibernate.Filters
{
    /// <summary>
    /// Add filter MayHaveTenant 
    /// 可能有多租户过滤器
    /// </summary>
    public class MayHaveTenantFilter : FilterDefinition
    {
        /// <summary>
        /// Contructor
        /// 构造函数
        /// </summary>
        public MayHaveTenantFilter()
        {
            WithName(AbpDataFilters.MayHaveTenant)
                .AddParameter("tenantId", NHibernateUtil.Int32)
                .WithCondition("TenantId = :tenantId )");
        }
    }
}