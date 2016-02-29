namespace Abp.Text.Formatting
{
    /// <summary>
    /// 格式化字符串标记
    /// </summary>
    internal class FormatStringToken
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 格式化字符串标记类型
        /// </summary>
        public FormatStringTokenType Type { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="type">格式化字符串标记类型</param>
        public FormatStringToken(string text, FormatStringTokenType type)
        {
            Text = text;
            Type = type;
        }
    }
}