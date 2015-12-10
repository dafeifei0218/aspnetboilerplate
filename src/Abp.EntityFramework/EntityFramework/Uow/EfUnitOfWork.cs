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
    /// EF的工作单元
    /// </summary>
    public class EfUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        //活动数据上下文集合
        protected readonly IDictionary<Type, DbContext> ActiveDbContexts;

        //IOC解析器
        protected IIocResolver IocResolver { get; private set; }

        //当前事务范围
        protected TransactionScope CurrentTransaction;

        /// <summary>
        /// Creates a new <see cref="EfUnitOfWork"/>.
        /// 构造函数
        /// </summary>
        public EfUnitOfWork(IIocResolver iocResolver, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            IocResolver = iocResolver;
            ActiveDbContexts = new Dictionary<Type, DbContext>();
        }

        /// <summary>
        /// 开始工作单元
        /// </summary>
        protected override void BeginUow()
        {
            //如果开启事务
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    //设置事务级别为：ReadUncommitted可以在事务期间读取和修改可变数据。
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    //获取或设置该事务的超时时间。
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                //设置事务
                CurrentTransaction = new TransactionScope(
                    Options.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                    transactionOptions,
                    Options.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                    );
            }
        }

        /// <summary>
        /// 保存变更
        /// </summary>
        public override void SaveChanges()
        {
            ActiveDbContexts.Values.ForEach(SaveChangesInDbContext);
        }

        /// <summary>
        /// 保存变更-异步
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
        /// 完成工作单元
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
        /// 完成工作单元-异步
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
        /// 禁用过滤器
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        protected override void ApplyDisableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.DisableFilter(filterName);
            }
        }

        /// <summary>
        /// 启用过滤器
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        protected override void ApplyEnableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.EnableFilter(filterName);
            }
        }

        /// <summary>
        /// 过滤参数值
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">值</param>
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
        /// 获取或创建数据上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据上下文类型</typeparam>
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
        /// 释放工作单元
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
        /// 根据数据上下文保存变更
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 根据数据上下文保存变更-异步
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        /// <returns></returns>
        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="TDbContext">数据上下文类型</typeparam>
        /// <returns></returns>
        protected virtual TDbContext Resolve<TDbContext>()
        {
            return IocResolver.Resolve<TDbContext>();
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocResolver.Release(dbContext);
        }
    }
}