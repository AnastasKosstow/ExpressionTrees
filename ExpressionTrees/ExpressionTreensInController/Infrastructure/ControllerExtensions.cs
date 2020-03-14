
namespace ExpressionTreesInController.Infrastructure
{
    using System;
    using System.Linq.Expressions;

    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult RedirectTo<T>(this Controller controller, Expression<Action<T>> redirect)
            where T : Controller
        {
            if (redirect.Body.NodeType != ExpressionType.Call)
                throw new InvalidOperationException($"{redirect.Body}");

            return GetRedirectFromExpression<T>(controller, redirect);
        }


        private static IActionResult GetRedirectFromExpression<TController>(Controller controller, LambdaExpression redirect)
        {
            MethodCallExpression methodCallExpression = (MethodCallExpression)redirect.Body;

            string actionName = GetActionName(methodCallExpression);
            string controllerName = GetControllerName<TController>();

            return controller.RedirectToAction(actionName, controllerName);
        }


        private static string GetActionName(MethodCallExpression methodCallExpression)
            =>
            methodCallExpression.Method.Name;


        private static string GetControllerName<TController>()
            => 
            typeof(TController).Name.Replace(nameof(Controller), string.Empty);
    }
}
