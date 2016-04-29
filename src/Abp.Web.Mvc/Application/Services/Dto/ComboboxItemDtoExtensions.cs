using System.Web.Mvc;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Combobox 项目数据传输 扩展类
    /// </summary>
    public static class ComboboxItemDtoExtensions
    {
        /// <summary>
        /// 转换为SelectListItem
        /// </summary>
        /// <param name="comboboxItem"></param>
        /// <returns></returns>
        public static SelectListItem ToSelectListItem(this ComboboxItemDto comboboxItem)
        {
            return new SelectListItem
            {
                Value = comboboxItem.Value,
                Text = comboboxItem.DisplayText,
                Selected = comboboxItem.IsSelected
            };
        }
    }
}
