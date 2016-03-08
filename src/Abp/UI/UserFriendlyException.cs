using System;
using System.Runtime.Serialization;

namespace Abp.UI
{
    /// <summary>
    /// This exception type is directly shown to the user.
    /// TODO: Move to Abp namespace
    /// �û��Ѻ��쳣
    /// </summary>
    [Serializable]
    public class UserFriendlyException : AbpException
    {
        /// <summary>
        /// Additional information about the exception.
        /// �����쳣�ĸ�����Ϣ
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// An arbitrary error code.
        /// ����������
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        public UserFriendlyException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// ���캯���������л�
        /// </summary>
        /// <param name="serializationInfo">���л���Ϣ</param>
        /// <param name="context">���л���������</param>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message �쳣��Ϣ</param>
        public UserFriendlyException(string message)
            : base(message)
        {

        }


        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="code">Error code �������</param>
        /// <param name="message">Exception message �쳣��Ϣ</param>
        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message �쳣��Ϣ</param>
        /// <param name="details">Additional information about the exception �����쳣�ĸ�����Ϣ</param>
        public UserFriendlyException(string message, string details)
            : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="code">Error code �������</param>
        /// <param name="message">Exception message �쳣��Ϣ</param>
        /// <param name="details">Additional information about the exception �����쳣�ĸ�����Ϣ</param>
        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message �쳣��Ϣ</param>
        /// <param name="innerException">Inner exception �ڲ��쳣</param>
        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message �쳣��Ϣ</param>
        /// <param name="details">Additional information about the exception �����쳣�ĸ�����Ϣ</param>
        /// <param name="innerException">Inner exception �ڲ��쳣</param>
        public UserFriendlyException(string message, string details, Exception innerException)
            : base(message, innerException)
        {
            Details = details;
        }
    }
}