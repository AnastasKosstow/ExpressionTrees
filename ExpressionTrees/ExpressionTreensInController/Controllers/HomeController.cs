
namespace ExpressionTreesInController.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ExpressionTreesInController.Models;

    public class HomeController : Controller
    {
        public IActionResult Index(string str, int intVal)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
