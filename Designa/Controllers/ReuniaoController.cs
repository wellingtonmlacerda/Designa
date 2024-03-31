using Designa.Data;
using Designa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Designa.Controllers
{
    public class ReuniaoController : Controller
    {
        public IPublicacao _publicacao;
        public List<Reuniao> _reuniao = new ();
        public IReuniaoFactory _reuniaoFactory;
        public readonly DesignaContext _context;
        public ReuniaoController(DesignaContext designaContext, IPublicacao publicacao, IReuniaoFactory reuniaoFactory)
        {
            _context = designaContext;
            _publicacao = publicacao;
            _reuniaoFactory = reuniaoFactory;
        }
        // GET: Reuniao
        public async Task<ActionResult> Index(int issui = 0)
        {
           await AtualizaBanco(issui);
            
            var reunioes = _context.Reunioes.Where(x => x.Issue == _publicacao.RetonaPubEmissao(issui))
                                            .Include(i => i.Partes);

            return View(reunioes);
        }

        public async Task AtualizaBanco(int issui = 0)
        {
            if (!_context.Reunioes.Any(x => x.Issue == _publicacao.RetonaPubEmissao(issui)))
            {
                _publicacao = await _publicacao.GetAsyncRoot(issui);

                if (_publicacao.RetornaListaRTF() is List<RTF> listaRTF && listaRTF.Count() > 0)
                {
                    foreach (var rtf in listaRTF.Where(x => x.Mimetype.Equals("application/rtf")))
                    {
                        var url = rtf.File.Url;
                        var stringRTF = await _publicacao.GetArquivo(url);

                        // Carrega o texto RTF
                        string textoCorrigido = _publicacao.CorrigirCaracteresEspeciaisRTF(stringRTF);
                        _reuniao.Add(_reuniaoFactory.CriaReuniao(textoCorrigido, rtf.Title, _publicacao.ThisPublicacao().Issue));

                        _context.AddRange(_reuniao);
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
