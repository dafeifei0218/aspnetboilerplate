using System.Collections.Generic;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// ���ػ���������ĿԴ�ӿ�
    /// </summary>
    public interface ILocalizableComboboxItemSource
    {
        /// <summary>
        /// ��Ŀ
        /// </summary>
        ICollection<ILocalizableComboboxItem> Items { get; }
    }
}