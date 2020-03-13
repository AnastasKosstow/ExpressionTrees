
namespace ExpressionTreesBasics.Helpers
{
    using System;
    using System.Reflection;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    using ExpressionTreesBasics.Classes;

    public static class Helpers
    {
        public static Action PrintBorderLine = 
            () => 
            Console.WriteLine("\n------------------------------\n");


        public static Expression<Func<int, int, int>> CreateBinaryExpression()
        {
            // Expression<Func<int, int, int>> binaryExpression = (x, y) => (x + y);

            ParameterExpression leftParameter = Expression.Parameter(typeof(int), "x");
            ParameterExpression rightParameter = Expression.Parameter(typeof(int), "y");

            BinaryExpression binaryExpression = Expression.Multiply(leftParameter, rightParameter);

            Expression<Func<int, int, int>> lambdaExpression = 
                Expression.Lambda<Func<int, int, int>>(
                    binaryExpression,
                    new List<ParameterExpression>() { leftParameter, rightParameter });

            return lambdaExpression;
        }


        public static LambdaExpression CreateMethodCallExpression()
        {
            /* Expression<Func<DemoClass, string>> lambdaExpression = x => x.DemoMethod(5, 10); */

            /* Creating the Parameter "X" on which the Method will be called. */
            var parameter = Expression.Parameter(typeof(DemoClass), "x");

            /* For now: x => () */

            /* Then we create the method. */
            /* First! The constants for the method. */
            var firstConstant = Expression.Constant(5);
            var secondConstant = Expression.Constant(10);

            MethodInfo method = typeof(DemoClass).GetMethod(nameof(DemoClass.DemoMethod));
            Expression[] arguments = new Expression[] { firstConstant, secondConstant };
            /* Then the method call itself. */
            /* The method call expression is invoked on a parameter. */
            var methodCall = Expression.Call(parameter, method, arguments);

            /* For now: x => () 
             * and second expression: x.DemoMethod(5, 10) */

            /* And now the two expressions are combined into a single lambda expression */
            var lambdaExpression = Expression.Lambda<Func<DemoClass, string>>(methodCall, parameter);

            /* And finally: 
             * Expression<Func<DemoClass, string>> lambdaExpression = x => x.DemoMethod(5, 10) */

            return lambdaExpression;
        }
    }
}
