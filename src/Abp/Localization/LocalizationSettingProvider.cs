using System.Collections.Generic;
using Abp.Configuration;

namespace Abp.Localization
{
    /// <summary>
    /// ���ػ������ṩ��
    /// </summary>
    public class LocalizationSettingProvider : SettingProvider
    {
        /// <summary>
        /// ��ȡ���ö����б�
        /// </summary>
        /// <param name="context">���ö����ṩ��������</param>
        /// <returns></returns>
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(LocalizationSettingNames.DefaultLanguage, null, L("DefaultLanguage"), scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true)
            };
        }

        /// <summary>
        /// ��ȡ���ػ�֧����
        /// </summary>
        /// <param name="name">���ػ��ַ���������</param>
        /// <returns>���ػ��ַ���</returns>
        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, AbpConsts.LocalizationSourceName);
        }
    }
}