
namespace ExpressionTreesInController.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    using static ExpressionTreesInController.Infrastructure.ExpressionParser;

    public static class ControllerExtensions
    {
        public static Task<IActionResult> RedirectTo<T>(this Controller controller, Expression<Action<T>> redirect)
            where T : Controller
        {
            if (redirect.Body.NodeType != ExpressionType.Call)
                throw new InvalidOperationException($"{redirect.Body}");

            return GetRedirectFromExpression<T>(controller, redirect);
        }


        private static async Task<IActionResult> GetRedirectFromExpression<TController>(Controller controller, LambdaExpression redirect)
        {
            MethodCallExpression methodCallExpression = (MethodCallExpression)redirect.Body;

            RouteValueDictionary routeValues = ExtractRouteValues(methodCallExpression);

            string actionName = GetActionName(methodCallExpression);
            string controllerName = GetControllerName<TController>();

            return await Task.Run(() => controller.RedirectToAction(actionName, controllerName, routeValues));
        }


        private static RouteValueDictionary ExtractRouteValues(MethodCallExpression methodCallExpression)
        {
            IEnumerable<string> parametersNames =
                methodCallExpression.Method.GetParameters().Select(param => param.Name);

            IEnumerable<object> @params =
                methodCallExpression.Arguments.Select(exp => GetParameterValue(exp));

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();

            return routeValueDictionary.RouteValues(parametersNames, @params);
        }


        public static RouteValueDictionary RouteValues(this RouteValueDictionary source,
            IEnumerable<string> parametersNames,
            IEnumerable<object> parameters)
        {
            return _(); RouteValueDictionary _()
            {
                RouteValueDictionary routeValues = new RouteValueDictionary();

                using var enumeratorNames = parametersNames.GetEnumerator();
                using var enumeratorParams = parameters.GetEnumerator();

                while (enumeratorNames.MoveNext() && enumeratorParams.MoveNext())
                {
                    routeValues.Add(enumeratorNames.Current, enumeratorParams.Current);
                }

                return routeValues;
            }
        }


        private static string GetActionName(MethodCallExpression methodCallExpression)
            =>
            methodCallExpression.Method.Name;


        private static string GetControllerName<TController>()
            => 
            typeof(TController).Name.Replace(nameof(Controller), string.Empty);
    }
}
