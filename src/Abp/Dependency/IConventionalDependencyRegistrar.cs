namespace Abp.Dependency
{
    /// <summary>
    /// This interface is used to register dependencies by conventions. 
    /// 常规依赖注册接口，这个接口是用来注册依赖关系的。
    /// </summary>
    /// <remarks>
    /// Implement this interface and register to <see cref="IocManager.AddConventionalRegistrar"/> method to be able
    /// to register classes by your own conventions.
    /// </remarks>
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// Registers types of given assembly by convention.
        /// 注册程序集，按常规方式注册类型给程序集
        /// </summary>
        /// <param name="context">Registration context 注册上下文</param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}