using System;

namespace Abp
{
    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValue : NameValue<string>
    {
        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// 名称/值，可用于存储名称/值（或键/值）对
    /// </summary>
    [Serializable]
    public class NameValue<T>
    {
        /// <summary>
        /// Name.
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// 构造函数
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public NameValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}