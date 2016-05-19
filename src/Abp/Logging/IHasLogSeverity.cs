namespace Abp.Logging
{
    /// <summary>
    /// Interface to define a <see cref="Severity"/> property (see <see cref="LogSeverity"/>).
    /// 日志的严重程度的接口。
    /// </summary>
    public interface IHasLogSeverity
    {
        /// <summary>
        /// Log severity.
        /// 日志的严重程度
        /// </summary>
        LogSeverity Severity { get; set; }
    }
}