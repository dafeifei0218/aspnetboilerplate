namespace Abp.Dependency
{
    /// <summary>
    /// Lifestyles of types used in dependency injection system.
    /// 依赖注入生命周期，
    /// 用于依赖注入系统的生命周期类型
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// Singleton object. Created a single object on first resolving
        /// and same instance is used for subsequent resolves.
        /// 单例对象。创建一个单独的对象和同一实例用于随后的解析。
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient object. Created one object for every resolving.
        /// 瞬态对象。为每个解决创建一个对象。
        /// </summary>
        Transient
    }
}