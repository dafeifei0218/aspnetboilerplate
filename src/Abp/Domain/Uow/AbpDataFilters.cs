using Abp.Domain.Entities;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Standard filters of ABP.
    /// Abp���ݹ�����
    /// </summary>
    public static class AbpDataFilters
    {
        /// <summary>
        /// "SoftDelete".
        /// Soft delete filter.
        /// Prevents getting deleted data from database.
        /// See <see cref="ISoftDelete"/> interface.
        /// ��ɾ��
        /// </summary>
        public const string SoftDelete = "SoftDelete";

        /// <summary>
        /// "MustHaveTenant".
        /// Tenant filter to prevent getting data that is
        /// not belong to current tenant.
        /// �������⻧
        /// �⻧���������Է�ֹ��õ����ݲ����ڵ�ǰ�⻧��
        /// </summary>
        public const string MustHaveTenant = "MustHaveTenant";

        /// <summary>
        /// "MayHaveTenant".
        /// Tenant filter to prevent getting data that is
        /// not belong to current tenant.
        /// �������⻧
        /// �⻧���������Է�ֹ��õ����ݲ����ڵ�ǰ�⻧��
        /// </summary>
        public const string MayHaveTenant = "MayHaveTenant";

        /// <summary>
        /// Standard parameters of ABP.
        /// ABPĬ�ϲ���
        /// </summary>
        public static class Parameters
        {
            /// <summary>
            /// "tenantId".
            /// �⻧Id
            /// </summary>
            public const string TenantId = "tenantId";
        }
    }
}