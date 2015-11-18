using System;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Defines a unit of work.
    /// This interface is internally used by ABP.
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
    /// ������Ԫ�ӿ�
    /// �˽ӿ�ABP�ڲ�ʹ��
    /// ʹ�� <see cref="IUnitOfWorkManager.Begin()"/> ��ʼһ���µĹ�����Ԫ
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// Unique id of this UOW.
        /// Ψһ�ı�ʶID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Reference to the outer UOW if exists.
        /// ��㹤����Ԫ
        /// </summary>
        IUnitOfWork Outer { get; set; }
        
        /// <summary>
        /// Begins the unit of work with given options.
        /// ��ʼ������Ԫ
        /// </summary>
        /// <param name="options">Unit of work options</param>
        void Begin(UnitOfWorkOptions options);
    }
}