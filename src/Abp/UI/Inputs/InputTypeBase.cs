using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Collections.Extensions;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 输入类型基类
    /// </summary>
    [Serializable]
    public abstract class InputTypeBase : IInputType
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name
        {
            get
            {
                var type = GetType();
                if (type.IsDefined(typeof(InputTypeAttribute)))
                {
                    return type.GetCustomAttributes(typeof(InputTypeAttribute)).Cast<InputTypeAttribute>().First().Name;
                }

                return type.Name;
            }
        }

        /// <summary>
        /// Gets/sets arbitrary objects related to this object.
        /// Gets null if given key does not exists.
        /// 索引器，
        /// 获取/设置与此对象相关的任意对象。
        /// 如果给定密钥不存在，为null
        /// </summary>
        /// <param name="key">Key 键</param>
        public object this[string key]
        {
            get { return Attributes.GetOrDefault(key); }
            set { Attributes[key] = value; }
        }

        /// <summary>
        /// Arbitrary objects related to this object.
        /// 属性字典，与此对象相关的任意对象
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }

        /// <summary>
        /// 值验证器
        /// </summary>
        public IValueValidator Validator { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected InputTypeBase()
            :this(new AlwaysValidValueValidator())
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validator">值验证器</param>
        protected InputTypeBase(IValueValidator validator)
        {
            Attributes = new Dictionary<string, object>();
            Validator = validator;
        }
    }
}