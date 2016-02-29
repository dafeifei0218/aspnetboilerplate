using System;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// ��֤�Զ�������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidatorAttribute : Attribute
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name"></param>
        public ValidatorAttribute(string name)
        {
            Name = name;
        }
    }
}