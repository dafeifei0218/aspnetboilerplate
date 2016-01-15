using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This handle is used for innet unit of work scopes.
    /// A inner unit of work scope actually uses outer unit of work scope
    /// and has no effect on <see cref="IUnitOfWorkCompleteHandle.Complete"/> call.
    /// But if it's not called, an exception is thrown at end of the UOW to rollback the UOW.
    /// 内部工作单元完成处理程序
    /// </summary>
    internal class InnerUnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {
        public const string DidNotCallCompleteMethodExceptionMessage = "Did not call Complete method of a unit of work.";

        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;

        /// <summary>
        /// 完成
        /// </summary>
        public void Complete()
        {
            _isCompleteCalled = true;
        }

        /// <summary>
        /// 异步完成
        /// </summary>
        /// <returns></returns>
        public async Task CompleteAsync()
        {
            _isCompleteCalled = true;           
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (!_isCompleteCalled)
            {
                if (HasException())
                {
                    return;
                }

                throw new AbpException(DidNotCallCompleteMethodExceptionMessage);
            }
        }
        
        /// <summary>
        /// 是否有异常
        /// </summary>
        /// <returns></returns>
        private static bool HasException()
        {
            try
            {
                return Marshal.GetExceptionCode() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}