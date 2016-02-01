namespace Abp.Localization
{
    /// <summary>
    /// Represents an available language for the application.
    /// 语言信息
    /// </summary>
    public class LanguageInfo
    {
        /// <summary>
        /// Code name of the language.
        /// It should be valid culture code.
        /// Ex: "en-US" for American English, "tr-TR" for Turkey Turkish.
        /// 语言代码名称，例如：“en-US”美国英语，“tr-TR”土耳其语
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the language in it's original language.
        /// Ex: "English" for English, "T黵k鏴" for Turkish.
        /// 显示名称，例如：“English”英语，“”
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// An icon can be set to display on the UI.
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Is this the default language?
        /// 是否是默认语言
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Creates a new <see cref="LanguageInfo"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="name">
        /// Code name of the language.
        /// It should be valid culture code.
        /// Ex: "en-US" for American English, "tr-TR" for Turkey Turkish.
        /// 语言代码名称
        /// </param>
        /// <param name="displayName">
        /// Display name of the language in it's original language.
        /// Ex: "English" for English, "T黵k鏴" for Turkish.
        /// 显示名称
        /// </param>
        /// <param name="icon">An icon can be set to display on the UI 图标</param>
        /// <param name="isDefault">Is this the default language? 是否默认语言</param>
        public LanguageInfo(string name, string displayName, string icon = null, bool isDefault = false)
        {
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            IsDefault = isDefault;
        }
    }
}