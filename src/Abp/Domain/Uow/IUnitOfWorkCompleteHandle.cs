using System;
using System.Threading.Tasks;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Used to complete a unit of work.
    /// This interface can not be injected or directly used.
    /// Use <see cref="IUnitOfWorkManager"/> instead.
    /// 工作单元完成操作
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        /// Completes this unit of work.
        /// It saves all changes and commit transaction if exists.
        /// 完成
        /// </summary>
        void Complete();

        /// <summary>
        /// Completes this unit of work.
        /// It saves all changes and commit transaction if exists.
        /// 异步完成
        /// </summary>
        Task CompleteAsync();
    }
}