using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abp.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// ģ����Ϣ
    /// </summary>
    internal class AbpModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// ģ��ĳ���
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Type of the module.
        /// ģ������
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Instance of the module.
        /// ģ��ʵ��
        /// </summary>
        public AbpModule Instance { get; private set; }

        /// <summary>
        /// All dependent modules of this module.
        /// ��������ģ�����е����ģ��
        /// </summary>
        public List<AbpModuleInfo> Dependencies { get; private set; }

        /// <summary>
        /// Creates a new AbpModuleInfo object.
        /// ģ����Ϣ
        /// </summary>
        /// <param name="instance">ģ��ʵ��</param>
        public AbpModuleInfo(AbpModule instance)
        {
            Dependencies = new List<AbpModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}