using Designa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Designa.Controllers
{
    public class ReuniaoController : Controller
    {
        public Publicacao _publicacao;
        public List<Reuniao> _reuniao;
        public ReuniaoController()
        {
            _publicacao = new Publicacao();
            _reuniao = new List<Reuniao>();
        }
        // GET: Reuniao
        public async Task<ActionResult> Index(int issui = 0)
        {
            if (issui != 0)
                _publicacao = await _publicacao.GetAsyncRoot(issui.ToString());
            else
                _publicacao = await _publicacao.GetAsyncRoot(issui);

            if (_publicacao.Files.T.RTF is List<RTF> listaRTF)
            {
                foreach (var rtf in listaRTF.Where(x => x.Mimetype.Equals("application/rtf")))
                {
                    var url = rtf.File.Url;
                    var stringRTF = await _publicacao.GetArquivo(url);

                    // Carrega o texto RTF
                    string textoCorrigido = _publicacao.CorrigirCaracteresEspeciaisRTF(stringRTF);
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
