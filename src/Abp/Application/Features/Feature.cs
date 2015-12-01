﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Collections.Extensions;
using Abp.Localization;
using Abp.UI.Inputs;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a feature of the application. A <see cref="Feature"/> can be used in a multi-tenant application
    /// to enable disable some application features depending on editions and tenants.
    /// 功能，定义应用程序的功能。可用于多租户应用程序的功能启用禁用某些应用程序功能
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Gets/sets arbitrary objects related to this object.
        /// These objects must be serializable.
        /// Gets null if given key does not exists.
        /// This is a shortcut for <see cref="Attributes"/> dictionary.
        /// 获取/设置与此对象相关的任意对象。
        /// 这些对象必须是可序列化的。
        /// 如果给定秘钥不存在为null。 
        /// </summary>
        /// <param name="key">Key</param>
        public object this[string key]
        {
            get { return Attributes.GetOrDefault(key); }
            set { Attributes[key] = value; }
        }

        /// <summary>
        /// Arbitrary objects related to this object.
        /// These objects must be serializable.
        /// 与此对象相关的任意对象。
        /// 这些对象必须是可序列化的。
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }

        /// <summary>
        /// Parent of this feature, if one exists.
        /// If set, this feature can be enabled only if parent is enabled.
        /// 父功能，如果父功能启用该功能也启用
        /// </summary>
        public Feature Parent { get; private set; }

        /// <summary>
        /// Unique name of the feature.
        /// 功能名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the feature.
        /// This can be used to show features on UI.
        /// 功能显示的名称。
        /// 这个可以用来显示用户界面的功能。
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// A brief description for this feature.
        /// This can be used to show feature description on UI. 
        /// 功能的描述
        /// </summary>
        public ILocalizableString Description { get; set; }
        
        /// <summary>
        /// Input type.
        /// This can be used to prepare an input for changing this feature's value.
        /// Default: <see cref="CheckboxInputType"/>.
        /// 输入类型
        /// </summary>
        public IInputType InputType { get; set; }

        /// <summary>
        /// Default value of the feature.
        /// This value is used if feature's value is not defined for current edition or tenant.
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Feature's scope.
        /// 功能范围
        /// </summary>
        public FeatureScopes Scope { get; set; }

        /// <summary>
        /// List of child features.
        /// 子动能列表
        /// </summary>
        public IReadOnlyList<Feature> Children
        {
            get { return _children.ToImmutableList(); }
        }
        private readonly List<Feature> _children;

        /// <summary>
        /// Creates a new feature.
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique name of the feature 功能名称</param>
        /// <param name="defaultValue">Default value 默认值</param>
        /// <param name="displayName">Display name of the feature 功能显示的名称</param>
        /// <param name="description">A brief description for this feature 功能的描述</param>
        /// <param name="scope">Feature scope 功能范围</param>
        /// <param name="inputType">Input type 输入类型</param>
        public Feature(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            DisplayName = displayName;
            Description = description;
            Scope = scope;
            DefaultValue = defaultValue;
            InputType = inputType ?? new CheckboxInputType();

            _children = new List<Feature>();
            Attributes = new Dictionary<string, object>();
        }

        /// <summary>
        /// Adds a child feature.
        /// 添加子功能
        /// </summary>
        /// <returns>Returns newly created child feature 返回新创建的子功能</returns>
        public Feature CreateChildFeature(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null)
        {
            var feature = new Feature(name, defaultValue, displayName, description, scope, inputType) { Parent = this };
            _children.Add(feature);
            return feature;
        }

        public override string ToString()
        {
            return string.Format("[Feature: {0}]", Name);
        }
    }
}
