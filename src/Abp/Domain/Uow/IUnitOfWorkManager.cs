using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Unit of work manager.
    /// Used to begin and control a unit of work.
    /// 工作单元管理接口
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// Gets currently active unit of work (or null if not exists).
        /// 获取当前活动的工作单元
        /// </summary>
        IActiveUnitOfWork Current { get; }

        /// <summary>
        /// Begins a new unit of work.
        /// 开始工作单元
        /// </summary>
        /// <returns>A handle to be able to complete the unit of work
        /// 工作单元完成操作</returns>
        IUnitOfWorkCompleteHandle Begin();

        /// <summary>
        /// Begins a new unit of work.
        /// 开始工作单元
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <returns>A handle to be able to complete the unit of work
        /// 工作单元完成操作</returns>
        IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope);

        /// <summary>
        /// Begins a new unit of work.
        /// 开始工作单元
        /// </summary>
        /// <param name="options">工作单元选项</param>
        /// <returns>A handle to be able to complete the unit of work
        /// 工作单元完成操作</returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options);
    }
}
