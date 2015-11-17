using System;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Defines a unit of work.
    /// This interface is internally used by ABP.
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
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