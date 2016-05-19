namespace Abp.Logging
{
    /// <summary>
    /// Indicates severity for log.
    /// 指示日志的严重程度。
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Debug.
        /// 调试。
        /// </summary>
        Debug,

        /// <summary>
        /// Info.
        /// 信息。
        /// </summary>
        Info,

        /// <summary>
        /// Warn.
        /// 警告。
        /// </summary>
        Warn,

        /// <summary>
        /// Error.
        /// 错误
        /// </summary>
        Error,

        /// <summary>
        /// Fatal.
        /// 致命的。
        /// </summary>
        Fatal
    }
}