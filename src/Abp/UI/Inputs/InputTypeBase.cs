using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Collections.Extensions;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class InputTypeBase : IInputType
    {
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="key">Key</param>
        public object this[string key]
        {
            get { return Attributes.GetOrDefault(key); }
            set { Attributes[key] = value; }
        }

        /// <summary>
        /// Arbitrary objects related to this object.
        /// 
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IValueValidator Validator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected InputTypeBase()
            :this(new AlwaysValidValueValidator())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validator"></param>
        protected InputTypeBase(IValueValidator validator)
        {
            Attributes = new Dictionary<string, object>();
            Validator = validator;
        }
    }
}