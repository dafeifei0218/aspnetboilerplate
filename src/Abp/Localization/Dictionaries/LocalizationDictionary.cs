using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Represents a simple implementation of <see cref="ILocalizationDictionary"/> interface.
    /// 本地化字典
    /// </summary>
    /// <remarks>
    /// 实现了ILocalizationDictionary和IEnumerable两个接口，他本身就是一个具有集合操作的类。
    /// 其内部封装了一个Dictionary的实例，用于提供真正的集合操作。
    /// 这个基类只提供了从其内部的Dictionary中根据原string查找返回本地化的string。 
    /// 其本身并没有将本地化资源文件中的数据加载到其内部的Dictionary的功能，这部分是在其子类中实现的。
    /// </remarks>
    public class LocalizationDictionary : ILocalizationDictionary, IEnumerable<LocalizedString>
    {
        /// <inheritdoc/>
        public CultureInfo CultureInfo { get; private set; }

        /// <inheritdoc/>
        public virtual string this[string name]
        {
            get
            {
                var localizedString = GetOrNull(name);
                return localizedString == null ? null : localizedString.Value;
            }
            set
            {
                _dictionary[name] = new LocalizedString(name, value, CultureInfo);
            }
        }

        private readonly Dictionary<string, LocalizedString> _dictionary;

        /// <summary>
        /// Creates a new <see cref="LocalizationDictionary"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="cultureInfo">Culture of the dictionary 提供有关特定区域性的信息</param>
        public LocalizationDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
            _dictionary = new Dictionary<string, LocalizedString>();
        }

        /// <inheritdoc/>
        public virtual LocalizedString GetOrNull(string name)
        {
            LocalizedString localizedString;
            return _dictionary.TryGetValue(name, out localizedString) ? localizedString : null;
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return _dictionary.Values.ToImmutableList();
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc/>
        public virtual IEnumerator<LocalizedString> GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        /// <summary>
        /// 是否包含指定的键
        /// </summary>
        /// <param name="name">键名称</param>
        /// <returns></returns>
        protected bool Contains(string name)
        {
            return _dictionary.ContainsKey(name);
        }
    }
}