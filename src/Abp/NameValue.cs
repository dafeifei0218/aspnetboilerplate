using System;

namespace Abp
{
    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// ����/ֵ�������ڴ洢����/ֵ�����/ֵ����
    /// </summary>
    [Serializable]
    public class NameValue
    {
        /// <summary>
        /// Name.
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// ֵ
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// ���캯��
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// ���캯��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="value">ֵ</param>
        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}