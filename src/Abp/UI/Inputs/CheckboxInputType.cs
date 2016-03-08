using System;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// ��ѡ��ѡ����������
    /// </summary>
    [Serializable]
    [InputType("CHECKBOX")]
    public class CheckboxInputType : InputTypeBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CheckboxInputType()
            : this(new BooleanValueValidator())
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="validator"></param>
        public CheckboxInputType(IValueValidator validator)
            : base(validator)
        {
            
        }
    }
}