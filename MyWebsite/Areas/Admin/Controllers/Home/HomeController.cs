using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Areas.Admin.Controllers.Home
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
