using System;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Defines a unit of work.
    /// This interface is internally used by ABP.
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
    /// 工作单元接口
    /// 此接口ABP内部使用
    /// 使用 <see cref="IUnitOfWorkManager.Begin()"/> 开始一个新的工作单元
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// Unique id of this UOW.
        /// 唯一的标识ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Reference to the outer UOW if exists.
        /// 外层工作单元
        /// </summary>
        IUnitOfWork Outer { get; set; }
        
        /// <summary>
        /// Begins the unit of work with given options.
        /// 开始工作单元
        /// </summary>
        /// <param name="options">Unit of work options</param>
        void Begin(UnitOfWorkOptions options);
    }
}