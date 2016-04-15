using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MongoDb.Configuration;
using MongoDB.Driver;

namespace Abp.MongoDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MongoDB.
    /// MongoDb工作单元
    /// </summary>
    public class MongoDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to MongoDB Database.
        /// 获取MongoDb数据库
        /// </summary>
        public MongoDatabase Database { get; private set; }

        /// <summary>
        /// AbpMongoDb模块配置
        /// </summary>
        private readonly IAbpMongoDbModuleConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="configuration">AbpMongoDb模块配置</param>
        /// <param name="defaultOptions">工作单元默认选项</param>
        public MongoDbUnitOfWork(IAbpMongoDbModuleConfiguration configuration, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 开始工作单元
        /// </summary>
        protected override void BeginUow()
        {
            Database = new MongoClient(_configuration.ConnectionString)
                .GetServer()
                .GetDatabase(_configuration.DatatabaseName);
        }

        /// <summary>
        /// 保存变更
        /// </summary>
        public override void SaveChanges()
        {

        }

        /// <summary>
        /// 保存变更-异步
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {

        }

        /// <summary>
        /// 完成工作单元
        /// </summary>
        protected override void CompleteUow()
        {

        }

        /// <summary>
        /// 完成工作单元-异步
        /// </summary>
        /// <returns></returns>
        protected override async Task CompleteUowAsync()
        {

        }

        /// <summary>
        /// 销毁工作单元
        /// </summary>
        protected override void DisposeUow()
        {

        }
    }
}