using Abp.Runtime.Validation;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is used to define DTOs those are used as input parameters.
    /// 输入数据传输对象接口，此接口定义DTOs使用哪些输入参数
    /// </summary>
    /// <remarks>
    /// 用于输入参数的DTO,该接口继承自IValidate。所以所有作为输入参数的DTO都会在使用前先Validate。
    /// </remarks>
    public interface IInputDto : IDto, IValidate
    {

    }
}