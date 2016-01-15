using System;

namespace Abp.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to resolve dependencies.
    /// IOC���Ʒ�ת������
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��IOC������ȡһ������
        /// ���ض��������ʹ�ú��ͷţ��뿴Release��
        /// </summary> 
        /// <typeparam name="T">Type of the object to get ���������</typeparam>
        /// <returns>The object instance ����ʵ��</returns>
        T Resolve<T>();

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��IOC������ȡһ������
        /// ���ض��������ʹ�ú��ͷţ��뿴Release�� 
        /// </summary> 
        /// <typeparam name="T">Type of the object to get ���������</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance ����ʵ��</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��IOC������ȡһ������
        /// ���ض��������ʹ�ú��ͷţ��뿴Release�� 
        /// </summary> 
        /// <param name="type">Type of the object to get ���������</param>
        /// <returns>The object instance ����ʵ��</returns>
        object Resolve(Type type);

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��IOC������ȡһ������
        /// ���ض��������ʹ�ú��ͷţ��뿴Release��  
        /// </summary> 
        /// <param name="type">Type of the object to get ���������</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments ���캯������</param>
        /// <returns>The object instance ����ʵ��</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);
        
        /// <summary>
        /// Releases a pre-resolved object. See Resolve methods.
        /// �ͷ�Ԥ������󣬼�Resolve����
        /// </summary>
        /// <param name="obj">Object to be released ��������</param>
        void Release(object obj);

        /// <summary>
        /// Checks whether given type is registered before.
        /// ��ע��ǰ�����������Ƿ�ע��
        /// </summary>
        /// <param name="type">Type to check �������</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// ��ע��ǰ�����������Ƿ�ע��
        /// </summary>
        /// <typeparam name="T">Type to check �������</typeparam>
        bool IsRegistered<T>();
    }
}