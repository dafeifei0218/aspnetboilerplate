using Abp.Dependency;
using Abp.Domain.Uow;
using MongoDB.Driver;

namespace Abp.MongoDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMongoDatabaseProvider"/> that gets database from active unit of work.
    /// 工作单元MongoDb数据库提供者
    /// </summary>
    public class UnitOfWorkMongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        /// <summary>
        /// Mongo数据库
        /// </summary>
        public MongoDatabase Database { get { return ((MongoDbUnitOfWork)_currentUnitOfWork.Current).Database; } }

        /// <summary>
        /// 当前工作单元提供者
        /// </summary>
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUnitOfWork">当前工作单元提供者</param>
        public UnitOfWorkMongoDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}