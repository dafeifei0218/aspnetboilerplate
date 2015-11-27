namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface can be used to mark a DTO as both of <see cref="IInputDto"/> and <see cref="IOutputDto"/>.
    /// 输入输出数据传输对象接口，继承IInputDto输入数据传输对象，IOutputDto数据传输对象
    /// </summary>
    public interface IDoubleWayDto : IInputDto, IOutputDto
    {

    }
}