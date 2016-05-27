using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// �����ṩ�߽ӿ�
    /// </summary>
    /// <remarks>
    /// �ӿڶ���һ�����ر������Լ��ϵķ�����
    /// ����ʹ�ýӿ����������б�Ҫ�ģ���ΪABP�ײ��ܵ�DefaultLanguageProviderֻ�Ƿ����ù�����HardCode��ϵͳ�е�LanguageInfo��Ϣ��
    /// �����Ҫ������Source���������ݿ⣩�л�ȡ���õ�LanguageInfo��Ϣ����ô���Ǿͱ���ʵ���Զ����LanguageProvider��
    /// </remarks>
    public interface ILanguageProvider
    {
        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}