using System;
using System.Reflection;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This attribute is used to indicate that declaring method is atomic and should be considered as a unit of work.
    /// A method that has this attribute is intercepted, a database connection is opened and a transaction is started before call the method.
    /// At the end of method call, transaction is commited and all changes applied to the database if there is no exception,
    /// othervise it's rolled back. 
    /// 工作单元自定义属性
    /// </summary>
    /// <remarks>
    /// This attribute has no effect if there is already a unit of work before calling this method, if so, it uses the same transaction.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// Scope option.
        /// 范围选项
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?
        /// Uses default value if not supplied.
        /// 是否UOW事务，
        /// </summary>
        public bool? IsTransactional { get; private set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.
        /// Uses default value if not supplied.
        /// 过期时间
        /// </summary>
        public TimeSpan? Timeout { get; private set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// Uses default value if not supplied.
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Used to prevent starting a unit of work for the method.
        /// If there is already a started unit of work, this property is ignored.
        /// Default: false.
        /// 是否禁用，
        /// 默认：false
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Creates a new UnitOfWorkAttribute object.
        /// 构造函数
        /// </summary>
        public UnitOfWorkAttribute()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="isTransactional">
        /// Is this unit of work will be transactional? 是否事务
        /// </param>
        public UnitOfWorkAttribute(bool isTransactional)
        {
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="timeout">As milliseconds 过期时间，分钟</param>
        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="isTransactional">Is this unit of work will be transactional? 是否事务</param>
        /// <param name="timeout">As milliseconds 过期时间，分钟</param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object. 
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// 构造函数
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level 事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// 构造函数
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level 事务隔离级别</param>
        /// <param name="timeout">Transaction  timeout as milliseconds 过期时间</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// 构造函数
        /// </summary>
        /// <param name="scope">Transaction scope 事务范围</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = true;
            Scope = scope;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// 构造函数
        /// </summary>
        /// <param name="scope">Transaction scope 事务范围</param>
        /// <param name="timeout">Transaction  timeout as milliseconds 过期时间</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Gets UnitOfWorkAttribute for given method or null if no attribute defined.
        /// 获取UnitOfWorkAttribute属性定义
        /// </summary>
        /// <param name="methodInfo">Method to get attribute 方法信息</param>
        /// <returns>The UnitOfWorkAttribute object</returns>
        internal static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false);
            if (attrs.Length > 0)
            {
                return (UnitOfWorkAttribute)attrs[0];
            }

            if (UnitOfWorkHelper.IsConventionalUowClass(methodInfo.DeclaringType))
            {
                return new UnitOfWorkAttribute(); //Default
            }

            return null;
        }

        /// <summary>
        /// 创建选项
        /// </summary>
        /// <returns></returns>
        internal UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}