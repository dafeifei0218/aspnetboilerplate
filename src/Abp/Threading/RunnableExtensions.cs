namespace Abp.Threading
{
    /// <summary>
    /// Some extension methods for <see cref="IRunnable"/>.
    /// IRunnable扩展方法
    /// </summary>
    public static class RunnableExtensions
    {
        /// <summary>
        /// Calls <see cref="IRunnable.Stop"/> and then <see cref="IRunnable.WaitToStop"/>.
        /// 调用停止并且等待停止
        /// </summary>
        public static void StopAndWaitToStop(this IRunnable runnable)
        {
            runnable.Stop();
            runnable.WaitToStop();
        }
    }
}