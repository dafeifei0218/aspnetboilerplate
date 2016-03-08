using System;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// ������������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InputTypeAttribute : Attribute
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">����</param>
        public InputTypeAttribute(string name)
        {
            Name = name;
        }
    }
}