using System;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// ������Чֵ��֤��
    /// </summary>
    [Validator("NULL")]
    [Serializable]
    public class AlwaysValidValueValidator : ValueValidatorBase
    {
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}