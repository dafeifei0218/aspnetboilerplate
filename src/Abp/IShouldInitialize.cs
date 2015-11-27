using Castle.Core;

namespace Abp
{
    /// <summary>
    /// Defines interface for objects those should be Initialized before using it.
    /// If the object resolved using dependency injection, <see cref="IInitializable.Initialize"/>
    /// method is automatically called just after creation of the object.
    /// 使用初始化接口
    /// 定义对象的接口，在使用它之前。
    /// 如果对象解决使用依赖注入， 方法是在创建对象之后自动调用的。
    /// </summary>
    public interface IShouldInitialize : IInitializable
    {
        
    }
}