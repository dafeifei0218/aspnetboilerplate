using System.Data.Entity;

namespace Abp.EntityFramework
{
    /// <summary>
    /// �������������ṩ��
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public sealed class SimpleDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// ����������
        /// </summary>
        public TDbContext DbContext { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dbContext"></param>
        public SimpleDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}