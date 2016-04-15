using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MongoDb.Configuration;
using MongoDB.Driver;

namespace Abp.MongoDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MongoDB.
    /// MongoDb������Ԫ
    /// </summary>
    public class MongoDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to MongoDB Database.
        /// ��ȡMongoDb���ݿ�
        /// </summary>
        public MongoDatabase Database { get; private set; }

        /// <summary>
        /// AbpMongoDbģ������
        /// </summary>
        private readonly IAbpMongoDbModuleConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="configuration">AbpMongoDbģ������</param>
        /// <param name="defaultOptions">������ԪĬ��ѡ��</param>
        public MongoDbUnitOfWork(IAbpMongoDbModuleConfiguration configuration, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ��ʼ������Ԫ
        /// </summary>
        protected override void BeginUow()
        {
            Database = new MongoClient(_configuration.ConnectionString)
                .GetServer()
                .GetDatabase(_configuration.DatatabaseName);
        }

        /// <summary>
        /// ������
        /// </summary>
        public override void SaveChanges()
        {

        }

        /// <summary>
        /// ������-�첽
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {

        }

        /// <summary>
        /// ��ɹ�����Ԫ
        /// </summary>
        protected override void CompleteUow()
        {

        }

        /// <summary>
        /// ��ɹ�����Ԫ-�첽
        /// </summary>
        /// <returns></returns>
        protected override async Task CompleteUowAsync()
        {

        }

        /// <summary>
        /// ���ٹ�����Ԫ
        /// </summary>
        protected override void DisposeUow()
        {

        }
    }
}