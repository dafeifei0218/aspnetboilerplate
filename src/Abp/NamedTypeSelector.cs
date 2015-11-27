using System;

namespace Abp
{
    /// <summary>
    /// Used to represent a named type selector.
    /// ��������ѡ����
    /// </summary>
    public class NamedTypeSelector
    {
        /// <summary>
        /// Name of the selector.
        /// ѡ��������
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Predicate.
        /// ����
        /// </summary>
        public Func<Type, bool> Predicate { get; set; }

        /// <summary>
        /// Creates new <see cref="NamedTypeSelector"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="name">Name ����</param>
        /// <param name="predicate">Predicate ����</param>
        public NamedTypeSelector(string name, Func<Type, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}