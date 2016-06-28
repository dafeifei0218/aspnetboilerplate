using Abp.Runtime.Validation;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is used to define DTOs those are used as input parameters.
    /// �������ݴ������ӿڣ��˽ӿڶ���DTOsʹ����Щ�������
    /// </summary>
    /// <remarks>
    /// �������������DTO,�ýӿڼ̳���IValidate������������Ϊ���������DTO������ʹ��ǰ��Validate��
    /// </remarks>
    public interface IInputDto : IDto, IValidate
    {

    }
}