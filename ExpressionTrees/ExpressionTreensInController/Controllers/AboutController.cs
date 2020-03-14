
namespace ExpressionTreesInController.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ExpressionTreesInController.Infrastructure;

    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return this.RedirectTo<HomeController>(c => c.Index());
        }
    }
}
