using Designa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Designa.Controllers
{
    public class ReuniaoController : Controller
    {
        public Raiz _raiz;
        public List<Reuniao> _reuniao;
        public ReuniaoController()
        {
            _raiz = new Raiz();
            _reuniao = new List<Reuniao>();
        }
        // GET: Reuniao
        public async Task<ActionResult> Index(int issui)
        {
            var _issui = issui.ToString();
            _raiz = await _raiz.GetAsyncRoot(_issui);
            if (_raiz.Files.T.RTF is List<RTF> listaRTF)
            {
                foreach (var rtf in listaRTF.Where(x => x.Mimetype.Equals("application/rtf")))
                {
                    var url = rtf.File.Url;
                    var stringRTF = await _raiz.GetArquivo(url);

                    // Carrega o texto RTF
                    string textoCorrigido = _raiz.CorrigirCaracteresEspeciaisRTF(stringRTF);
                    _reuniao.Add(new Reuniao(textoCorrigido)
                    {
                        Semana = rtf.Title
                    });
                }
            }
            return View(_reuniao);
        }
    }
}
