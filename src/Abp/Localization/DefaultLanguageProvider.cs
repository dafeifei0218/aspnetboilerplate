using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization
{
    /// <summary>
    /// Ĭ�������ṩ�� 
    /// </summary>
    /// <remarks>
    /// ��LocatizationConfiguration��ȡLanguageInfo����
    /// </remarks>
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
    {
        private readonly ILocalizationConfiguration _configuration;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration">���ػ�������</param>
        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns>������Ϣ�б�</returns>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}