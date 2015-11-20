using System;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// Add this class to a DbContext to define auto-repository types for entities in this DbContext.
    /// This is useful if you inherit same DbContext by more than one DbContext.
    /// Abp�ִ������Զ�������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        /// <summary>
        /// Ĭ�ϲִ�����
        /// </summary>
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        /// <summary>
        /// �ִ��ӿ�����
        /// </summary>
        public Type RepositoryInterface { get; private set; }

        /// <summary>
        /// �������Ĳִ��ӿ�����
        /// </summary>
        public Type RepositoryInterfaceWithPrimaryKey { get; private set; }

        /// <summary>
        /// �ִ�ʵ������
        /// </summary>
        public Type RepositoryImplementation { get; private set; }

        /// <summary>
        /// �������Ĳִ�ʵ������
        /// </summary>
        public Type RepositoryImplementationWithPrimaryKey { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        static AutoRepositoryTypesAttribute()
        {
            Default = new AutoRepositoryTypesAttribute(
                typeof (IRepository<>),
                typeof (IRepository<,>),
                typeof (EfRepositoryBase<,>),
                typeof (EfRepositoryBase<,,>)
                );
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="repositoryInterface">�ִ��ӿ�����</param>
        /// <param name="repositoryInterfaceWithPrimaryKey">�������Ĳִ��ӿ�����</param>
        /// <param name="repositoryImplementation">�ִ�ʵ������</param>
        /// <param name="repositoryImplementationWithPrimaryKey">�������Ĳִ�ʵ������</param>
        public AutoRepositoryTypesAttribute(
            Type repositoryInterface, 
            Type repositoryInterfaceWithPrimaryKey, 
            Type repositoryImplementation, 
            Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}