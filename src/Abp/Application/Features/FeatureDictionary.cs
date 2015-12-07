using System.Collections.Generic;
using System.Linq;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used to store <see cref="Feature"/>s.
    /// �����ֵ�
    /// </summary>
    public class FeatureDictionary : Dictionary<string, Feature>
    {
        /// <summary>
        /// Adds all child features of current features recursively.
        /// �ݹ������ӹ���
        /// </summary>
        public void AddAllFeatures()
        {
            foreach (var feature in Values.ToList())
            {
                AddFeatureRecursively(feature);
            }
        }

        /// <summary>
        /// �ݹ���ӹ���
        /// </summary>
        /// <param name="feature">����</param>
        private void AddFeatureRecursively(Feature feature)
        {
            //Prevent multiple adding of same named feature.
            //��ֹ�����ͬ�������ܵ����
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
            //����ӹ��ܣ��ݹ���ã�
            foreach (var childFeature in feature.Children)
            {
                AddFeatureRecursively(childFeature);
            }
        }
    }
}