using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Abp.Collections.Extensions;
using Abp.Reflection;

namespace Abp.Runtime.Validation.Interception
{
    /// <summary>
    /// This class is used to validate a method call (invocation) for method arguments.
    /// 方法调用验证器
    /// </summary>
    internal class MethodInvocationValidator
    {
        private readonly MethodInfo _method;
        private readonly object[] _parameterValues;
        private readonly ParameterInfo[] _parameters;
        private readonly List<ValidationResult> _validationErrors;

        /// <summary>
        /// Creates a new <see cref="MethodInvocationValidator"/> instance.
        /// 构造函数
        /// </summary>
        /// <param name="method">Method to be validated 方法验证</param>
        /// <param name="parameterValues">List of arguments those are used to call the <paramref name="method"/>. 用于调用方法的参数列表</param>
        public MethodInvocationValidator(MethodInfo method, object[] parameterValues)
        {
            _method = method;
            _parameterValues = parameterValues;
            _parameters = method.GetParameters();
            _validationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Validates the method invocation.
        /// 验证方法调用
        /// </summary>
        public void Validate()
        {
            if (!_method.IsPublic)
            {
                //Validate only public methods!
                //仅验证公共方法
                return;
            }

            if (_method.IsDefined(typeof (DisableValidationAttribute)))
            {
                //Don't validate if explicitly requested!
                //如果显示请求，请不要验证
                return;                
            }

            if (_parameters.IsNullOrEmpty())
            {
                //Object has no parameter, no need to validate.
                //对象没有参数，无需验证。
                return;
            }

            if (_parameters.Length != _parameterValues.Length)
            {
                //This is not possible actually
                //实际上这是不可能的
                //方法参数计数和参数技术不匹配
                throw new Exception("Method parameter count does not match with argument count!");
            }

            for (var i = 0; i < _parameters.Length; i++)
            {
                ValidateMethodParameter(_parameters[i], _parameterValues[i]);
            }

            if (_validationErrors.Any())
            {
                throw new AbpValidationException(
                    "Method arguments are not valid! See ValidationErrors for details.",
                    _validationErrors
                    );
            }

            foreach (var parameterValue in _parameterValues)
            {
                NormalizeParameter(parameterValue);
            }
        }

        /// <summary>
        /// Validates given parameter for given value.
        /// 给定值的给定参数的验证
        /// </summary>
        /// <param name="parameterInfo">Parameter of the method to validate 验证方法的参数</param>
        /// <param name="parameterValue">Value to validate 参数值，值验证</param>
        private void ValidateMethodParameter(ParameterInfo parameterInfo, object parameterValue)
        {
            if (parameterValue == null)
            {
                if (!parameterInfo.IsOptional && !parameterInfo.IsOut && !TypeHelper.IsPrimitiveExtendedIncludingNullable(parameterInfo.ParameterType))
                {
                    _validationErrors.Add(new ValidationResult(parameterInfo.Name + " is null!", new[] { parameterInfo.Name }));
                }

                return;
            }

            ValidateObjectRecursively(parameterValue);
        }

        /// <summary>
        /// 递归验证对象
        /// </summary>
        /// <param name="validatingObject">验证对象</param>
        private void ValidateObjectRecursively(object validatingObject)
        {
            if (validatingObject is IEnumerable && !(validatingObject is IQueryable))
            {
                foreach (var item in (validatingObject as IEnumerable))
                {
                    ValidateObjectRecursively(item);
                }
            }

            if (!(validatingObject is IValidate))
            {
                return;
            }

            SetValidationAttributeErrors(validatingObject);

            if (validatingObject is ICustomValidate)
            {
                (validatingObject as ICustomValidate).AddValidationErrors(_validationErrors);
            }

            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                ValidateObjectRecursively(property.GetValue(validatingObject));
            }
        }

        /// <summary>
        /// Checks all properties for DataAnnotations attributes.
        /// 检查所有属性的DataAnnotations的属性
        /// </summary>
        /// <param name="validatingObject">验证对象</param>
        private void SetValidationAttributeErrors(object validatingObject)
        {
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject)
                {
                    DisplayName = property.Name,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result != null)
                    {
                        _validationErrors.Add(result);
                    }
                }
            }
        }

        /// <summary>
        /// 常规参数
        /// </summary>
        /// <param name="parameterValue">参数值</param>
        private static void NormalizeParameter(object parameterValue)
        {
            if (parameterValue is IShouldNormalize)
            {
                (parameterValue as IShouldNormalize).Normalize();
            }
        }
    }
}
