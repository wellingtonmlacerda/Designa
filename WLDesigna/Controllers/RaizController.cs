using Microsoft.AspNetCore.Mvc;
using WLDesigna.Models;

namespace WLDesigna.Controllers
{
    public class RaizController : Controller
    {
        public Raiz _raiz;
        public RaizController() 
        {
            _raiz = new Raiz();
        }
        public IActionResult Index()
        {
            return View(_raiz);
        }
    }
}
