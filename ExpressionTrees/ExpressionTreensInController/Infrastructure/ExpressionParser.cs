
namespace ExpressionTreesInController.Infrastructure
{
    using System.Reflection;
    using System.Linq.Expressions;

    internal class ExpressionParser
    {
        public static object GetParameterValue(Expression expression)
        {
            object objectValue = default;

            if (expression.NodeType == ExpressionType.Constant)
            {
                objectValue = ((ConstantExpression)expression).Value;
            }

            else if (expression.NodeType == ExpressionType.MemberAccess && ((MemberExpression)expression).Member is FieldInfo)
            {
                MemberExpression memberExpression = (MemberExpression)expression;
                ConstantExpression constantExpression = (ConstantExpression)memberExpression.Expression;

                if (constantExpression != null)
                {
                    var memberName = memberExpression.Member.Name;
                    var lambdaScopeField = constantExpression.Value.GetType().GetField(memberName);
                    objectValue = lambdaScopeField.GetValue(constantExpression.Value);
                }
            }

            return objectValue;
        }
    }
}
