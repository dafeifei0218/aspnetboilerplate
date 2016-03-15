using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Defines interface to store/get background jobs.
    /// ��̨�����洢�ӿڣ�����ӿ����洢/���ú�̨����
    /// </summary>
    public interface IBackgroundJobStore
    {
        /// <summary>
        /// Inserts a background job.
        /// �����̨����-�첽
        /// </summary>
        /// <param name="jobInfo">Job information. ��̨������Ϣ</param>
        Task InsertAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Gets waiting jobs. It should get jobs based on these:
        /// Conditions: !IsAbandoned && NextTryTime &lt;= Clock.Now.
        /// Order by: Priority DESC, TryCount ASC, NextTryTime ASC.
        /// Maximum result: <see cref="maxResultCount"/>.
        /// ��ȡ�ȴ�����,
        /// ��Ӧ�õõ������Ļ�������Щ��
        /// ������
        /// �������ȼ��������������´�����
        /// �������<see cref="maxResultCount"/>
        /// </summary>
        /// <param name="maxResultCount">Maximum result count. �������</param>
        Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount);

        /// <summary>
        /// Deletes a job.
        /// ɾ������-�첽
        /// </summary>
        /// <param name="jobInfo">Job information. ��̨������Ϣ</param>
        Task DeleteAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Updates a job.
        /// ���¹���-�첽
        /// </summary>
        /// <param name="jobInfo">Job information. ��̨������Ϣ</param>
        Task UpdateAsync(BackgroundJobInfo jobInfo);
    }
}