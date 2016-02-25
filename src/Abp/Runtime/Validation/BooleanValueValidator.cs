using System;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// 布尔值验证器
    /// </summary>
    [Serializable]
    [Validator("BOOLEAN")]
    public class BooleanValueValidator : ValueValidatorBase
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            //如果为null，返回false
            if (value == null)
            {
                return false;
            }

            //如果是bool类型，返回true
            if (value is bool)
            {
                return true;
            }

            bool b;
            return bool.TryParse(value.ToString(), out b);
        }
    }
}