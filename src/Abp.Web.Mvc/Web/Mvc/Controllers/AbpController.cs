using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Logging;
using Abp.Reflection;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Web.Models;
using Abp.Web.Mvc.Controllers.Results;
using Abp.Web.Mvc.Models;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Abp.Web.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Abp system.
    /// Abp系统MVC控制器基类
    /// </summary>
    public abstract class AbpController : Controller
    {
        /// <summary>
        /// Gets current session information.
        /// 获取当前Session信息
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Gets the event bus.
        /// 获取时间总线。
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Reference to the permission manager.
        /// 权限管理类
        /// </summary>
        public IPermissionManager PermissionManager { get; set; }

        /// <summary>
        /// Reference to the setting manager.
        /// 设置管理类
        /// </summary>
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// 权限检查
        /// </summary>
        public IPermissionChecker PermissionChecker { protected get; set; }

        /// <summary>
        /// Reference to the feature manager.
        /// 特征管理类
        /// </summary>
        public IFeatureManager FeatureManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// 特征检查
        /// </summary>
        public IFeatureChecker FeatureChecker { protected get; set; }

        /// <summary>
        /// Reference to the localization manager.
        /// 本地化管理类
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// 获取/设置本地化源名称用于应用程序服务
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// 获取本地化源
        /// </summary>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new AbpException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Reference to the logger to write logs.
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets current session information.
        /// 获取当前Session信息。
        /// </summary>
        [Obsolete("Use AbpSession property instead. CurrentSession will be removed in future releases.")]
        protected IAbpSession CurrentSession { get { return AbpSession; } }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// 工作单元管理类
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new AbpException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// 获取当前工作单元
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// 审计配置
        /// </summary>
        public IAuditingConfiguration AuditingConfiguration { get; set; }

        /// <summary>
        /// 审计信息提供者
        /// </summary>
        public IAuditInfoProvider AuditInfoProvider { get; set; }

        /// <summary>
        /// 审计存储
        /// </summary>
        public IAuditingStore AuditingStore { get; set; }

        /// <summary>
        /// This object is used to measure an action execute duration.
        /// 动作执行时间，这个对象是用来衡量一个动作的执行时间。
        /// </summary>
        private Stopwatch _actionStopwatch;

        private AuditInfo _auditInfo;

        /// <summary>
        /// MethodInfo for currently executing action.
        /// 当前正在执行的工作的方法信息。
        /// </summary>
        private MethodInfo _currentMethodInfo;

        /// <summary>
        /// WrapResultAttribute for currently executing action.
        /// 包装结果属性，当前正在执行的动作包装结果属性。
        /// </summary>
        private WrapResultAttribute _wrapResultAttribute;

        /// <summary>
        /// 
        /// </summary>
        private static Type[] _ignoredTypesForSerialization = {typeof (HttpPostedFileBase)};

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        protected AbpController()
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
            AuditingStore = SimpleLogAuditingStore.Instance;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// 获取给定键名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// 获取给定键名称和当前语言格式的本地化字符串。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="args">Format arguments 格式化参数</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// 获取给定键名称和特定区域信息的本地化字符串
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 提供有关特定区域性信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// 获取给定键名称和特定区域信息的本地化字符串
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 提供有关特定区域性信息</param>
        /// <param name="args">Format arguments 格式化参数</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// 检查当前用户是否授予权限-异步
        /// </summary>
        /// <param name="permissionName">Name of the permission 权限名称</param>
        protected Task<bool> IsGrantedAsync(string permissionName)
        {
            return PermissionChecker.IsGrantedAsync(permissionName);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// 检查当前用户是否授予权限
        /// </summary>
        /// <param name="permissionName">Name of the permission 权限名称</param>
        protected bool IsGranted(string permissionName)
        {
            return PermissionChecker.IsGranted(permissionName);
        }


        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// 检查当前租户是否启用给定的特征-异步
        /// </summary>
        /// <param name="featureName">Name of the feature 特征名称</param>
        /// <returns></returns>
        protected virtual Task<bool> IsEnabledAsync(string featureName)
        {
            return FeatureChecker.IsEnabledAsync(featureName);
        }

        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// 检查当前租户是否启用给定的特征
        /// </summary>
        /// <param name="featureName">Name of the feature 特征名称</param>
        /// <returns></returns>
        protected virtual bool IsEnabled(string featureName)
        {
            return FeatureChecker.IsEnabled(featureName);
        }

        /// <summary>
        /// Json the specified data, contentType, contentEncoding and behavior.
        /// Json的指定数据，内容类型，内容编码和行为。
        /// </summary>
        /// <param name="data">Data. 数据</param>
        /// <param name="contentType">Content type. 内容类型</param>
        /// <param name="contentEncoding">Content encoding. 内容字符串编码</param>
        /// <param name="behavior">Behavior. 行为</param>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (_wrapResultAttribute != null && !_wrapResultAttribute.WrapOnSuccess)
            {
                return base.Json(data, contentType, contentEncoding, behavior);
            }

            if (data == null)
            {
                data = new AjaxResponse();
            }
            else if (!ReflectionHelper.IsAssignableToGenericType(data.GetType(), typeof(AjaxResponse<>)))
            {
                data = new AjaxResponse(data);
            }
            
            return new AbpJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #region OnActionExecuting / OnActionExecuted
        
        /// <summary>
        /// 动作执行时
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetCurrentMethodInfoAndWrapResultAttribute(filterContext);
            HandleAuditingBeforeAction(filterContext);

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 动作执行后
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            HandleAuditingAfterAction(filterContext);
        }

        /// <summary>
        /// 设置当前方法信息和包装结果自定义属性
        /// </summary>
        /// <param name="filterContext"></param>
        private void SetCurrentMethodInfoAndWrapResultAttribute(ActionExecutingContext filterContext)
        {
            _currentMethodInfo = ActionDescriptorHelper.GetMethodInfo(filterContext.ActionDescriptor);
            _wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrNull<WrapResultAttribute>(_currentMethodInfo) ??
                WrapResultAttribute.Default;
        }

        #endregion

        #region Exception handling

        /// <summary>
        /// 出现异常时
        /// </summary>
        /// <param name="context"></param>
        protected override void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //If exception handled before, do nothing.
            //If this is child action, exception should be handled by main action.
            if (context.ExceptionHandled || context.IsChildAction)
            {
                base.OnException(context);
                return;
            }

            //Log exception
            if (_wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (!context.HttpContext.IsCustomErrorEnabled)
            {
                base.OnException(context);
                return;
            }

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, context.Exception).GetHttpCode() != 500)
            {
                base.OnException(context);
                return;
            }

            //Check WrapResultAttribute
            if (!_wrapResultAttribute.WrapOnError)
            {
                base.OnException(context);
                return;
            }

            //We handled the exception!
            context.ExceptionHandled = true;

            //Return a special error response to the client.
            context.HttpContext.Response.Clear();
            context.Result = IsJsonResult()
                ? GenerateJsonExceptionResult(context)
                : GenerateNonJsonExceptionResult(context);

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            context.HttpContext.Response.TrySkipIisCustomErrors = true;

            //Trigger an event, so we can register it.
            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));
        }

        /// <summary>
        /// 是否Json结果
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsJsonResult()
        {
            return typeof (JsonResult).IsAssignableFrom(_currentMethodInfo.ReturnType) ||
                   typeof (Task<JsonResult>).IsAssignableFrom(_currentMethodInfo.ReturnType);
        }

        /// <summary>
        /// 通用Json异常结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual ActionResult GenerateJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 200; //TODO: Consider to return 500
            return new AbpJsonResult(
                new MvcAjaxResponse(
                    ErrorInfoBuilder.Instance.BuildForException(context.Exception),
                    context.Exception is AbpAuthorizationException
                    )
                );
        }

        /// <summary>
        /// 通用非Json异常结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual ActionResult GenerateNonJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(context.Exception)),
                TempData = context.Controller.TempData
            };
        }

        #endregion
        
        #region Auditing

        /// <summary>
        /// 动作前审计
        /// </summary>
        /// <param name="filterContext"></param>
        private void HandleAuditingBeforeAction(ActionExecutingContext filterContext)
        {
            if (!ShouldSaveAudit(filterContext))
            {
                _auditInfo = null;
                return;
            }

            _actionStopwatch = Stopwatch.StartNew();
            _auditInfo = new AuditInfo
            {
                TenantId = AbpSession.TenantId,
                UserId = AbpSession.UserId,
                ImpersonatorUserId = AbpSession.ImpersonatorUserId,
                ImpersonatorTenantId = AbpSession.ImpersonatorTenantId,
                ServiceName = _currentMethodInfo.DeclaringType != null
                                ? _currentMethodInfo.DeclaringType.FullName
                                : filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                MethodName = _currentMethodInfo.Name,
                Parameters = ConvertArgumentsToJson(filterContext.ActionParameters),
                ExecutionTime = Clock.Now
            };
        }

        /// <summary>
        /// 动作后审计
        /// </summary>
        /// <param name="filterContext"></param>
        private void HandleAuditingAfterAction(ActionExecutedContext filterContext)
        {
            if (_auditInfo == null || _actionStopwatch == null)
            {
                return;
            }

            _actionStopwatch.Stop();

            _auditInfo.ExecutionDuration = Convert.ToInt32(_actionStopwatch.Elapsed.TotalMilliseconds);
            _auditInfo.Exception = filterContext.Exception;

            if (AuditInfoProvider != null)
            {
                AuditInfoProvider.Fill(_auditInfo);
            }

            AuditingStore.Save(_auditInfo);
        }

        /// <summary>
        /// 应该保存审计
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool ShouldSaveAudit(ActionExecutingContext filterContext)
        {
            if (AuditingConfiguration == null)
            {
                return false;
            }

            if (!AuditingConfiguration.MvcControllers.IsEnabled)
            {
                return false;
            }

            if (filterContext.IsChildAction && !AuditingConfiguration.MvcControllers.IsEnabledForChildActions)
            {
                return false;
            }

            return AuditingHelper.ShouldSaveAudit(
                _currentMethodInfo,
                AuditingConfiguration,
                AbpSession,
                true
                );
        }

        /// <summary>
        /// 转换参数到Json
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    if (argument.Value != null && _ignoredTypesForSerialization.Any(t => t.IsInstanceOfType(argument.Value)))
                    {
                        dictionary[argument.Key] = null;
                    }
                    else
                    {
                        dictionary[argument.Key] = argument.Value;                        
                    }
                }

                return JsonConvert.SerializeObject(
                    dictionary,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            catch (Exception ex)
            {
                Logger.Warn("Could not serialize arguments for method: " + _auditInfo.ServiceName + "." + _auditInfo.MethodName);
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        #endregion
    }
}