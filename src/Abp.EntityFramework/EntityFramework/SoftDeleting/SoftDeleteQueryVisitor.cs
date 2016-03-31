using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;

namespace Abp.EntityFramework.SoftDeleting
{
    /// <summary>
    /// ��ɾ����ѯ������
    /// </summary>
    internal class SoftDeleteQueryVisitor : DefaultExpressionVisitor
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="expression">���ʽ</param>
        /// <returns></returns>
        public override DbExpression Visit(DbScanExpression expression)
        {
            //if (!expression.Target.ElementType.MetadataProperties.Any(mp => mp.Name.EndsWith("customannotation:" + AbpEfConsts.SoftDeleteCustomAnnotationName)))
            if (!expression.Target.ElementType.MetadataProperties.Any(mp => mp.Name.EndsWith("customannotation:" + "��ʱ�޸�Ϊ���ַ���������û��AbpEfConsts��")))
            {
                return base.Visit(expression);
            }

            var binding = expression.Bind();
            return binding.Filter(
                binding.VariableType.Variable(binding.VariableName)
                    .Property("IsDeleted") //TODO: User may want to bind to another column name. It's better to use actual database column name
                    .Equal(DbExpression.FromBoolean(false))
                );
        }
    }
}