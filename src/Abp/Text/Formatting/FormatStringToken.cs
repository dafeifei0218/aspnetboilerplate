namespace Abp.Text.Formatting
{
    /// <summary>
    /// 
    /// </summary>
    internal class FormatStringToken
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public FormatStringTokenType Type { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public FormatStringToken(string text, FormatStringTokenType type)
        {
            Text = text;
            Type = type;
        }
    }
}