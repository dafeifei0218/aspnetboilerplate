namespace Abp.Threading.BackgroundWorkers
{
    /// <summary>
    /// Interface for a worker (thread) that runs on background to perform some tasks.
    /// 后台工作接口，一个在后台运行的工作（线程）的接口来执行某些任务。
    /// </summary>
    public interface IBackgroundWorker : IRunnable
    {

    }
}
