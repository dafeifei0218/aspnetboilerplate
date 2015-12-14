using System.Linq;
using System.Reflection;
using Abp.Runtime.Session;

namespace Abp.Auditing
{
    /// <summary>
    /// 审计帮助类
    /// </summary>
    public static class AuditingHelper
    {
        /// <summary>
        /// 应该保存审计
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <param name="configuration">审计配置</param>
        /// <param name="abpSession">Abp会话</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool ShouldSaveAudit(MethodInfo methodInfo, IAuditingConfiguration configuration, IAbpSession abpSession, bool defaultValue = false)
        {
            //审计配置为null，或审计配置未启用，则返回false
            if (configuration == null || !configuration.IsEnabled)
            {
                return false;
            }

            //如果未启用匿名用户，并且AbpSession为null或用户Id为空，返回false
            if (!configuration.IsEnabledForAnonymousUsers && (abpSession == null || !abpSession.UserId.HasValue))
            {
                return false;
            }

            //如果方法信息为null
            if (methodInfo == null)
            {
                return false;
            }

            //如果方法信息不为公共方法
            if (!methodInfo.IsPublic)
            {
                return false;
            }

            //如果方法使用了AuditedAttribute审计自定义属性，则返回true
            if (methodInfo.IsDefined(typeof(AuditedAttribute)))
            {
                return true;
            }

            //如果方法使用了DisableAuditingAttribute禁用审计自定义属性，则返回false
            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute)))
            {
                return false;
            }

            //获取声明该成员的类
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