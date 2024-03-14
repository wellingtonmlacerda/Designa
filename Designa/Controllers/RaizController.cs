using Designa.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.rtf.document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SautinSoft;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Designa.Controllers
{
    public class RaizController : Controller
    {
        public Raiz _raiz;
        public List<Reuniao> _reuniao;
        public RaizController()
        {
            _raiz = new Raiz();
            _reuniao = new List<Reuniao>();
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                _raiz = await _raiz.GetAsyncRoot();
                if (_raiz.Files.T.RTF is List<RTF> listaRTF) {
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
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(_raiz);
        }
    }
}
