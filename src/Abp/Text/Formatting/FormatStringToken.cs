namespace Abp.Text.Formatting
{
    /// <summary>
    /// ��ʽ���ַ������
    /// </summary>
    internal class FormatStringToken
    {
        /// <summary>
        /// �ı�
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// ��ʽ���ַ����������
        /// </summary>
        public FormatStringTokenType Type { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="text">�ı�</param>
        /// <param name="type">��ʽ���ַ����������</param>
        public FormatStringToken(string text, FormatStringTokenType type)
        {
            Text = text;
            Type = type;
        }
    }
}