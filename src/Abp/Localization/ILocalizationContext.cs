namespace Abp.Localization
{
    /// <summary>
    /// Localization context.
    /// 本地化上下文。
    /// </summary>
    /// <remarks>
    /// 一般用作方法调用时的参数。
    /// </remarks>
    public interface ILocalizationContext
    {
        /// <summary>
        /// Gets the localization manager.
        /// 获取本地化管理类。
        /// </summary>
        ILocalizationManager LocalizationManager { get; }
    }
}