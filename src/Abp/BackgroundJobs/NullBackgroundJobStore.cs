using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Null pattern implementation of <see cref="IBackgroundJobStore"/>.
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// and job execution is not enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// �պ�̨�����洢�࣬ʵ��<see cref="IBackgroundJobStore"/>
    /// </summary>
    public class NullBackgroundJobStore : IBackgroundJobStore
    {
        /// <summary>
        /// �����̨����-�첽
        /// </summary>
        /// <param name="jobInfo">��̨������Ϣ</param>
        /// <returns></returns>
        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ�ȴ�����
        /// </summary>
        /// <param name="maxResultCount">�������</param>
        /// <returns></returns>
        public Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            return Task.FromResult(new List<BackgroundJobInfo>());
        }

        /// <summary>
        /// ɾ������-�첽
        /// </summary>
        /// <param name="jobInfo">��̨������Ϣ</param>
        /// <returns></returns>
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ���¹���-�첽
        /// </summary>
        /// <param name="jobInfo">��̨������Ϣ</param>
        /// <returns></returns>
        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }
    }
}