using System;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IComparable{T}"/>.
    /// IComparable�ȽϷ�����չ��
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// Checks a value is between a minimum and maximum value.
        /// ���ֵ�Ƿ�����С�����ֵ֮��
        /// </summary>
        /// <param name="value">The value to be checked Ҫ����ֵ</param>
        /// <param name="minInclusiveValue">Minimum (inclusive) value ��Сֵ</param>
        /// <param name="maxInclusiveValue">Maximum (inclusive) value ���ֵ</param>
        public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
        {
            return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
        }
    }
}