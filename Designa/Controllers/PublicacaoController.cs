using Designa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Designa.Controllers
{
    public class PublicacaoController : Controller
    {
        public Publicacao _publicacao;
        public PublicacaoController()
        {
            _publicacao = new Publicacao();
        }
        public async Task<ActionResult> Index()
        {
            List<Publicacao> publicacoes = new ();
            try
            {
                for (int i = -3; i <= 6; i++)
                {
                   publicacoes.Add(await _publicacao.GetAsyncRoot(i));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(publicacoes);  
        }
    }
}
