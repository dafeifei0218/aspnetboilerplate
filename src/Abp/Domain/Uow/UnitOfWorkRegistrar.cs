using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This class is used to register interceptor for needed classes for Unit Of Work mechanism.
    /// ������Ԫע����
    /// </summary>
    /// ͨ��UnitOfWorkRegistrar��UnitOfWorkInterceptor��ĳ���౻ע�ᵽIOCContainner��ʱ��
    /// һ����ӵ������������ж�Ӧ��ComponentModel��Interceptors�����С�
    /// �ܽ�һ�仰���ǣ�UOW�Ĺ�����ͨ���Զ���Castle��������ʵ�ֵġ�
    /// ������Ҫ����ABP���Ŀ���е�UnitOfWork��ʵ�֣�������ֱ����ABP����ģ������ξ���ʵ��IUnitOfWork��
    internal static class UnitOfWorkRegistrar
    {
        /// <summary>
        /// Initializes the registerer.
        /// ��ʼ��ע��
        /// </summary>
        /// <param name="iocManager">IOC manager</param>
        public static void Initialize(IIocManager iocManager)
        {
            //�÷�������Ӧ�ó���������ʱ����ã������¼�ע��
            iocManager.IocContainer.Kernel.ComponentRegistered += ComponentRegistered;
        }

        /// <summary>
        /// ����ע���¼�
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="handler"></param>
        private static void ComponentRegistered(string key, IHandler handler)
        {
            //�ж������Ƿ�ʵ����IRepository��IApplicationService������ǣ���Ϊ������ע����������UnitOfWorkInterceptor��
            if (UnitOfWorkHelper.IsConventionalUowClass(handler.ComponentModel.Implementation))
            {
                //Intercept all methods of all repositories.
                //����ȫ���ִ���ȫ������
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            else if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
            {
                //�����������κ�һ��������Ӧ����UnitOfWorkAttribute��ͬʱΪ����ע����������UnitOfWorkInterceptor��
                //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
                //TODO: Intecept only UnitOfWork methods, not other methods!
                //ֻ������UnitOfWork������û������������
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
    }
}