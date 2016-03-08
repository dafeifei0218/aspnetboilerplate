using System;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// �����ַ�����������
    /// </summary>
    [Serializable]
    [InputType("SINGLE_LINE_STRING")]
    public class SingleLineStringInputType : InputTypeBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public SingleLineStringInputType()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="validator">��֤��</param>
        public SingleLineStringInputType(IValueValidator validator)
            : base(validator)
        {
        }
    }
}