using Microsoft.AspNetCore.Mvc;

namespace WLDesigna.Controllers
{
    public class RootController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
