using Abp.Runtime.Validation;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is used to define DTOs those are used as input parameters.
    /// 输入数据传输对象接口，此接口定义DTOs使用哪些输入参数
    /// </summary>
    public interface IInputDto : IDto, IValidate
    {

    }
}