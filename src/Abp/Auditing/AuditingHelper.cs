using System.Linq;
using System.Reflection;
using Abp.Runtime.Session;

namespace Abp.Auditing
{
    /// <summary>
    /// ��ư�����
    /// </summary>
    /// <remarks>
    /// ��Щ������ִ�е�ʱ��ᱻ��������Auditing�����أ�����ɲμ�AuditingHelper�Ĵ��롣
    /// ����ܽ�����������ͬʱ���㣩��
    /// 1.��������AuditingConfiguration�е�IsEnabledΪtrue
    /// 2.���sessionΪ�գ���configuration.IsEnabledForAnonymousUsersҲ����Ϊtrue
    /// 3.Public ����
    /// </remarks>
    public static class AuditingHelper
    {
        /// <summary>
        /// Ӧ�ñ������
        /// </summary>
        /// <param name="methodInfo">������Ϣ</param>
        /// <param name="configuration">�������</param>
        /// <param name="abpSession">Abp�Ự</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns></returns>
        public static bool ShouldSaveAudit(MethodInfo methodInfo, IAuditingConfiguration configuration, IAbpSession abpSession, bool defaultValue = false)
        {
            //�������Ϊnull�����������δ���ã��򷵻�false
            if (configuration == null || !configuration.IsEnabled)
            {
                return false;
            }

            //���δ���������û�������AbpSessionΪnull���û�IdΪ�գ�����false
            if (!configuration.IsEnabledForAnonymousUsers && (abpSession == null || !abpSession.UserId.HasValue))
            {
                return false;
            }

            //���������ϢΪnull
            if (methodInfo == null)
            {
                return false;
            }

            //���������Ϣ��Ϊ��������
            if (!methodInfo.IsPublic)
            {
                return false;
            }

            //�������ʹ����AuditedAttribute����Զ������ԣ��򷵻�true
            if (methodInfo.IsDefined(typeof(AuditedAttribute)))
            {
                return true;
            }

            //�������ʹ����DisableAuditingAttribute��������Զ������ԣ��򷵻�false
            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute)))
            {
                return false;
            }

            //��ȡ�����ó�Ա����
            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.IsDefined(typeof(AuditedAttribute)))
                {
                    return true;
                }

                if (classType.IsDefined(typeof(DisableAuditingAttribute)))
                {
                    return false;
                }

                if (configuration.Selectors.Any(selector => selector.Predicate(classType)))
                {
                    return true;
                }
            }

            return defaultValue;
        }
    }
}