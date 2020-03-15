
namespace ExpressionTreesInController.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ExpressionTreesInController.Infrastructure;

    using System.Threading.Tasks;

    public class AboutController : Controller
    {
        public Task<IActionResult> Index()
        {
            string str = "SomeString";
            int intVal = 11;

            return this.RedirectTo<HomeController>(c => c.Index(str, 11));
        }
    }
}
