namespace Abp.Modules
{
    /// <summary>
    /// 模块管理类接口
    /// </summary>
    internal interface IAbpModuleManager
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        void InitializeModules();

        /// <summary>
        /// 关闭模块
        /// </summary>
        void ShutdownModules();
    }
}