using System.Data.Entity;

namespace Abp.EntityFramework
{
    /// <summary>
    /// DbContext�����������ṩ��
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// ����������
        /// </summary>
        TDbContext DbContext { get; }
    }
}