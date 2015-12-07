using System.Collections.Generic;
using System.Linq;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used to store <see cref="Feature"/>s.
    /// 功能字典
    /// </summary>
    public class FeatureDictionary : Dictionary<string, Feature>
    {
        /// <summary>
        /// Adds all child features of current features recursively.
        /// 递归所有子功能
        /// </summary>
        public void AddAllFeatures()
        {
            foreach (var feature in Values.ToList())
            {
                AddFeatureRecursively(feature);
            }
        }

        /// <summary>
        /// 递归添加功能
        /// </summary>
        /// <param name="feature">功能</param>
        private void AddFeatureRecursively(Feature feature)
        {
            //Prevent multiple adding of same named feature.
            //防止多个相同命名功能的添加
            Feature existingFeature;
            if (TryGetValue(feature.Name, out existingFeature))
            {
                if (existingFeature != feature)
                {
                    throw new AbpInitializationException("Duplicate feature name detected for " + feature.Name);
                }
            }
            else
            {
                this[feature.Name] = feature;
            }

            //Add child features (recursive call)
            //添加子功能（递归调用）
            foreach (var childFeature in feature.Children)
            {
                AddFeatureRecursively(childFeature);
            }
        }
    }
}