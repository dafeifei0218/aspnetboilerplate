using System;
using System.Data.Entity;
using Abp.Domain.Uow;

namespace Abp.EntityFramework.Uow
{
    /// <summary>
    /// Extension methods for UnitOfWork.
    /// 工作单元扩展
    /// </summary>
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// Gets a DbContext as a part of active unit of work.
        /// This method can be called when current unit of work is an <see cref="EfUnitOfWork"/>.
        /// 获取数据上下文
        /// </summary>
        /// <typeparam name="TDbContext">Type of the DbContext 数据上下文</typeparam>
        /// <param name="unitOfWork">Current (active) unit of work 当前（活动）的工作单元</param>
        public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork) 
            where TDbContext : DbContext
        {
            //如果当前（活动）的工作单元为null，抛出异常
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            //如果当前工作不为EfUnitOfWork，抛出异常
            if (!(unitOfWork is EfUnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(EfUnitOfWork).FullName, "unitOfWork");
            }

            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}