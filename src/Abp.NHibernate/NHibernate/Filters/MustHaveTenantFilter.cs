using Abp.Domain.Uow;
using FluentNHibernate.Mapping;
using NHibernate;

namespace Abp.NHibernate.Filters
{
    /// <summary>
    /// Add filter MustHaveTenant 
    /// 必须有租户过滤器
    /// </summary>
    public class MustHaveTenantFilter : FilterDefinition
    {
        /// <summary>
        /// Contructor
        /// 构造函数
        /// </summary>
        public MustHaveTenantFilter()
        {
            WithName(AbpDataFilters.MustHaveTenant)
                .AddParameter("tenantId", NHibernateUtil.Int32)
                .WithCondition("TenantId = :tenantId");
        }
    }
}