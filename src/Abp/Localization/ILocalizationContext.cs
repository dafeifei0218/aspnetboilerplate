namespace Abp.Localization
{
    /// <summary>
    /// Localization context.
    /// ���ػ������ġ�
    /// </summary>
    /// <remarks>
    /// һ��������������ʱ�Ĳ�����
    /// </remarks>
    public interface ILocalizationContext
    {
        /// <summary>
        /// Gets the localization manager.
        /// ��ȡ���ػ������ࡣ
        /// </summary>
        ILocalizationManager LocalizationManager { get; }
    }
}