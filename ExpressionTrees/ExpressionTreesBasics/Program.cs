
namespace ExpressionTreesBasics
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using ExpressionTreesBasics.Classes;

    using static ExpressionTreesBasics.Helpers.Helpers;

    public class Program
    {
        /// <summary>
        /// Demo code for: 
        /// </summary>
        static void Main()
        {
            Expression<Func<int, int, int>> lambdaExp = (x, y) => (x + y);
            Expression<Func<DemoClass, int>> propExp = x => x.IntValue;

            
            ExamineTheExpression(propExp);

            PrintBorderLine();

            ExamineTheExpression(lambdaExp);

            PrintBorderLine();


            /* Creating Expressions */
            /* See Helpers! */

            var methodCallExpression = CreateMethodCallExpression();

            ExamineTheExpression(methodCallExpression);

            PrintBorderLine();

            var binaryExpression = CreateBinaryExpression();

            ExamineTheExpression(binaryExpression);

            PrintBorderLine();
        }


        private static void ExamineTheExpression(Expression expression)
        {
            /* Describes a lambda expression. This captures a block of code that is similar to a .NET method body. */
            if (expression.NodeType == ExpressionType.Lambda)
            {
                var lambdaExpression = (LambdaExpression)expression;

                Console.WriteLine($"Lambda: {lambdaExpression}");

                ExamineTheExpression(lambdaExpression.Body);
            }


            /* Represents a call to either static or an instance method. */
            else if (expression.NodeType == ExpressionType.Call)
            {
                var methodCallExpression = (MethodCallExpression)expression;

                Console.WriteLine($"Method: {methodCallExpression.Method.Name}");

                if (methodCallExpression.Arguments.Count != 0)
                {
                    ParseArgs<ConstantExpression>();
                }
                else
                {
                    var binaryExpression = (BinaryExpression)methodCallExpression.Object;

                    ExamineTheExpression(binaryExpression);
                }
                

                void ParseArgs<T>() where T : Expression
                {
                    var @params = methodCallExpression.Arguments
                        .Select(x => (T)x);

                    foreach (var p in @params)
                        ExamineTheExpression(p);
                }
            }


            /* Represents a named parameter expression. */
            else if (expression.NodeType == ExpressionType.Parameter)
            {
                var parameterExpression = (ParameterExpression)expression;

                Console.WriteLine($"Parameter: {parameterExpression.ToString()}");
            }


            /* Represents an expression that has a constant value. */
            else if (expression.NodeType == ExpressionType.Constant)
            {
                var constantExpression = (ConstantExpression)expression;

                Console.WriteLine($"Constant: {constantExpression.Value}");
            }


            /* Represents accessing a field or property. */
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = (MemberExpression)expression;

                Console.WriteLine($"Member: {memberExpression.Member.Name}");
            }


            /* Represents an expression that has a binary operator. */
            else if (expression.NodeType == ExpressionType.Add ||
                     expression.NodeType == ExpressionType.Multiply) // Binary expression
            {
                var binaryExpression = (BinaryExpression)expression;

                Console.WriteLine(
                    $"Lef: {binaryExpression.Left} \n" +
                    $"Operator: {binaryExpression.NodeType} \n" +
                    $"Right: {binaryExpression.Right}");
            }
        }
    }
}
