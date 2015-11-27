using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Can be used to send/receive Name/Value (or Key/Value) pairs.
    /// ����/ֵ���ݴ�����󣬿����ڷ���/���գ�����/ֵ�����/ֵ���ԡ�
    /// </summary>
    [Serializable]
    public class NameValueDto : NameValue, IDto
    {
        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// ���캯��
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// ���캯��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="value">ֵ</param>
        public NameValueDto(string name, string value)
            : base(name, value)
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// ���캯��
        /// </summary>
        /// <param name="nameValue">A <see cref="NameValue"/> object to get it's name and value ����/ֵ�Ķ���</param>
        public NameValueDto(NameValue nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }
}