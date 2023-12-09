using Microsoft.AspNetCore.Mvc;
using WLDesigna.Models;

namespace WLDesigna.Controllers
{
    public class RaizController_old : Controller
    {
        public Raiz _raiz;
        public RaizController_old() 
        {
            _raiz = new Raiz();
        }
        public IActionResult Index()
        {
            return View(_raiz);
        }
    }
}
