using Designa.DAL;
using Designa.Data;
using Designa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using static Designa.Helpers.Enums;

namespace Designa.Controllers
{
    public class ReuniaoController : Controller
    {
        public IPublicacao _publicacao;
        public List<Reuniao> _listaReuniao = new();
        public IReuniaoFactory _reuniaoFactory;
        private readonly IGenericRepository<Reuniao> _reuniao;
        private readonly IGenericRepository<Publicador> _publicador;
        public ReuniaoController
        (
            IGenericRepository<Reuniao> reuniao,
            IPublicacao publicacao,
            IReuniaoFactory reuniaoFactory,
            IGenericRepository<Publicador> publicador
        )
        {
            _publicacao = publicacao;
            _reuniaoFactory = reuniaoFactory;
            _reuniao = reuniao;
            _publicador = publicador;
        }
        // GET: Reuniao
        public async Task<ActionResult> Index(int issue = 0, int pagina = 0)
        {
            int numeroPagina = pagina == 0 ? 1 : pagina;
            await AtualizaBanco(issue);

            var reunioes = await _reuniao.GetListWithIncludesAsync(x => x.Issue == _publicacao.RetonaPubEmissao(issue),
                                                                   p => p.Include(i => i.Partes).ThenInclude(t => t.PublicadorParte!.Publicador)
                                                                        .Include(t => t.Partes).ThenInclude(y => y.PublicadorParte!.PublicadorAjudante));

            return View(await reunioes.ToPagedListAsync(numeroPagina, 1));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(List<PublicadorParte> value)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task AtualizaBanco(int issue = 0)
        {
            try
            {
                if (!_reuniao.Any(x => x.Issue == _publicacao.RetonaPubEmissao(issue)))
                {
                    _publicacao = await _publicacao.GetAsyncRoot(issue);

                    if (_publicacao.RetornaListaRTF() is List<RTF> listaRTF && listaRTF.Count() > 0)
                    {
                        foreach (var rtf in listaRTF.Where(x => x.Mimetype.Equals("application/rtf")))
                        {
                            var url = rtf.File.Url;
                            var stringRTF = await _publicacao.GetArquivo(url);

                            // Carrega o texto RTF
                            string textoCorrigido = _publicacao.CorrigirCaracteresEspeciaisRTF(stringRTF);
                            _listaReuniao.Add(_reuniaoFactory.CriaReuniao(textoCorrigido, rtf.Title, _publicacao.ThisPublicacao().Issue));

                            _reuniao.AddRange(_listaReuniao);
                        }
                    }
                    await _reuniao.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
        }
        private async Task CarregaPaisAsync()
        {
            ViewBag["Publicadores"] = await _publicador.GetListAsync(x => x.Status == StatusPublicador.Ativo);
        }
    }
}
