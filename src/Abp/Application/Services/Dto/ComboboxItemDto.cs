using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be used as a simple item for a combobox/list.
    /// 组合框/列表的项目数据传输对象
    /// </summary>
    [Serializable]
    public class ComboboxItemDto : IDto
    {
        /// <summary>
        /// Value of the item.
        /// 项目值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Display text of the item.
        /// 项目显示文本
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Is selected?
        /// 是否选择
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// 构造函数
        /// </summary>
        public ComboboxItemDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// 构造函数
        /// </summary>
        /// <param name="value">Value of the item项目值 </param>
        /// <param name="displayText">Display text of the item 项目显示文本</param>
        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}