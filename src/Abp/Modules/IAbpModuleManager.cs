namespace Abp.Modules
{
    /// <summary>
    /// ģ�������ӿ�
    /// </summary>
    internal interface IAbpModuleManager
    {
        /// <summary>
        /// ��ʼ��ģ��
        /// </summary>
        void InitializeModules();

        /// <summary>
        /// �ر�ģ��
        /// </summary>
        void ShutdownModules();
    }
}