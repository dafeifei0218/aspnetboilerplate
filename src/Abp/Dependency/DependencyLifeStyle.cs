namespace Abp.Dependency
{
    /// <summary>
    /// Lifestyles of types used in dependency injection system.
    /// ����ע���������ڣ�
    /// ��������ע��ϵͳ��������������
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// Singleton object. Created a single object on first resolving
        /// and same instance is used for subsequent resolves.
        /// �������󡣴���һ�������Ķ����ͬһʵ���������Ľ�����
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient object. Created one object for every resolving.
        /// ˲̬����Ϊÿ���������һ������
        /// </summary>
        Transient
    }
}