using Designa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Designa.Controllers
{
    public class RaizController : Controller
    {
        public Raiz _raiz;
        public RaizController()
        {
            _raiz = new Raiz();
        }
        public async Task<ActionResult> Index()
        {
            List<Raiz> raiz = new ();
            try
            {
                for (int i = -3; i < 6; i++)
                {
                   raiz.Add(await _raiz.GetAsyncRoot(i));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(raiz);  
        }
    }
}
