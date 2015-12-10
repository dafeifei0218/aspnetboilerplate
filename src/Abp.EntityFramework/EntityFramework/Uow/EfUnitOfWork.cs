using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Reflection;
using Castle.Core.Internal;
using EntityFramework.DynamicFilters;

namespace Abp.EntityFramework.Uow
{
    /// <summary>
    /// Implements Unit of work for Entity Framework.
    /// EF�Ĺ�����Ԫ
    /// </summary>
    public class EfUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        //����������ļ���
        protected readonly IDictionary<Type, DbContext> ActiveDbContexts;

        //IOC������
        protected IIocResolver IocResolver { get; private set; }

        //��ǰ����Χ
        protected TransactionScope CurrentTransaction;

        /// <summary>
        /// Creates a new <see cref="EfUnitOfWork"/>.
        /// ���캯��
        /// </summary>
        public EfUnitOfWork(IIocResolver iocResolver, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            IocResolver = iocResolver;
            ActiveDbContexts = new Dictionary<Type, DbContext>();
        }

        /// <summary>
        /// ��ʼ������Ԫ
        /// </summary>
        protected override void BeginUow()
        {
            //�����������
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    //�������񼶱�Ϊ��ReadUncommitted�����������ڼ��ȡ���޸Ŀɱ����ݡ�
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    //��ȡ�����ø�����ĳ�ʱʱ�䡣
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                //��������
                CurrentTransaction = new TransactionScope(
                    Options.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                    transactionOptions,
                    Options.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                    );
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public override void SaveChanges()
        {
            ActiveDbContexts.Values.ForEach(SaveChangesInDbContext);
        }

        /// <summary>
        /// ������-�첽
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        /// <summary>
        /// ��ɹ�����Ԫ
        /// </summary>
        protected override void CompleteUow()
        {
            SaveChanges();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

        /// <summary>
        /// ��ɹ�����Ԫ-�첽
        /// </summary>
        /// <returns></returns>
        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

        /// <summary>
        /// ���ù�����
        /// </summary>
        /// <param name="filterName">����������</param>
        protected override void ApplyDisableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.DisableFilter(filterName);
            }
        }

        /// <summary>
        /// ���ù�����
        /// </summary>
        /// <param name="filterName">����������</param>
        protected override void ApplyEnableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.EnableFilter(filterName);
            }
        }

        /// <summary>
        /// ���˲���ֵ
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <param name="parameterName">��������</param>
        /// <param name="value">ֵ</param>
        protected override void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                if (TypeHelper.IsFunc<object>(value))
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, (Func<object>)value);
                }
                else
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, value);
                }
            }
        }

        /// <summary>
        /// ��ȡ�򴴽�����������
        /// </summary>
        /// <typeparam name="TDbContext">��������������</typeparam>
        /// <returns></returns>
        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {
                dbContext = Resolve<TDbContext>();

                foreach (var filter in Filters)
                {
                    if (filter.IsEnabled)
                    {
                        dbContext.EnableFilter(filter.FilterName);
                    }
                    else
                    {
                        dbContext.DisableFilter(filter.FilterName);
                    }

                    foreach (var filterParameter in filter.FilterParameters)
                    {
                        if (TypeHelper.IsFunc<object>(filterParameter.Value))
                        {
                            dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, (Func<object>)filterParameter.Value);
                        }
                        else
                        {
                            dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, filterParameter.Value);
                        }
                    }
                }

                ActiveDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        /// <summary>
        /// �ͷŹ�����Ԫ
        /// </summary>
        protected override void DisposeUow()
        {
            ActiveDbContexts.Values.ForEach(Release);

            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
            }
        }

        /// <summary>
        /// �������������ı�����
        /// </summary>
        /// <param name="dbContext">����������</param>
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// �������������ı�����-�첽
        /// </summary>
        /// <param name="dbContext">����������</param>
        /// <returns></returns>
        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="TDbContext">��������������</typeparam>
        /// <returns></returns>
        protected virtual TDbContext Resolve<TDbContext>()
        {
            return IocResolver.Resolve<TDbContext>();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="dbContext">����������</param>
        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocResolver.Release(dbContext);
        }
    }
}