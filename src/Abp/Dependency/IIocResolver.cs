using System;

namespace Abp.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to resolve dependencies.
    /// IOC控制反转解析器
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// 从IOC容器获取一个对象
        /// 返回对象必须在使用后释放（请看Release）
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 对象的类型</typeparam>
        /// <returns>The object instance 对象实例</returns>
        T Resolve<T>();

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// 从IOC容器获取一个对象
        /// 返回对象必须在使用后释放（请看Release） 
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance 对象实例</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// 从IOC容器获取一个对象
        /// 返回对象必须在使用后释放（请看Release） 
        /// </summary> 
        /// <param name="type">Type of the object to get 对象的类型</param>
        /// <returns>The object instance 对象实例</returns>
        object Resolve(Type type);

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// 从IOC容器获取一个对象
        /// 返回对象必须在使用后释放（请看Release）  
        /// </summary> 
        /// <param name="type">Type of the object to get 对象的类型</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments 构造函数参数</param>
        /// <returns>The object instance 对象实例</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);
        
        /// <summary>
        /// Releases a pre-resolved object. See Resolve methods.
        /// 释放预处理对象，见Resolve方法
        /// </summary>
        /// <param name="obj">Object to be released 发布对象</param>
        void Release(object obj);

        /// <summary>
        /// Checks whether given type is registered before.
        /// 在注册前检查给定类型是否注册
        /// </summary>
        /// <param name="type">Type to check 检查类型</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// 在注册前检查给定类型是否注册
        /// </summary>
        /// <typeparam name="T">Type to check 检查类型</typeparam>
        bool IsRegistered<T>();
    }
}