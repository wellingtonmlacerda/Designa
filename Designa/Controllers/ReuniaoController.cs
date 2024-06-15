using AutoMapper;
using Designa.Data;
using Designa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
        private readonly IGenericRepository<Parte> _parte;
        private readonly IGenericRepository<Publicador> _publicador;
        private readonly IGenericRepository<PublicadorParte> _publicadorParte;
        public ReuniaoController
        (
            IGenericRepository<Reuniao> reuniao,
            IPublicacao publicacao,
            IReuniaoFactory reuniaoFactory,
            IGenericRepository<Publicador> publicador,
            IGenericRepository<PublicadorParte> publicadorParte,
            IGenericRepository<Parte> parte
            //IMapper mapper
        )
        {
            _publicacao = publicacao;
            _reuniaoFactory = reuniaoFactory;
            _reuniao = reuniao;
            _publicador = publicador;
            _publicadorParte = publicadorParte;
            _parte = parte;
        }
        // GET: Reuniao
        public async Task<ActionResult> Index(int issue = 0, int pagina = 0)
        {
            int numeroPagina = pagina == 0 ? 1 : pagina;
            await AtualizaBanco(issue);

            var reunioes = await _reuniao.GetListWithIncludesAsync(x => x.Issue == _publicacao.RetonaPubEmissao(issue),
                                                                   p => p.Include(i => i.Partes).ThenInclude(t => t.PublicadorParte).ThenInclude(f => f!.Publicador)
                                                                        .Include(t => t.Partes).ThenInclude(y => y.PublicadorParte).ThenInclude(f => f!.PublicadorAjudante)
                                                                        .Include(pre => pre.Presidente)
                                                                        .Include(r => r.PublicadorOracao));

            var publicadores = await _publicador.GetListWithIncludesAsync(x => x.Status == EnumStatusPublicador.Ativo, y => y.Include(i => i.PublicadorPrivilegios));

            publicadores = publicadores.ToList();

            ViewBag.Presidentes = publicadores.Where(x => x.Sexo.Contains("M") && x.PublicadorPrivilegios!.Any(y => y.Privilegio == EnumPrivilegio.Anciao));
            ViewBag.PublicadorOracao = publicadores.Where(x => x.Sexo.Contains("M"));
            ViewBag.Publicadores = publicadores;

            return View(await reunioes.ToPagedListAsync(numeroPagina, reunioes.Count()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(List<Reuniao> reuniaos)
        {
            try
            {
                foreach (var reuniao in reuniaos)
                {
                    reuniao.Presidente = _publicador.GetId(reuniao.PresidenteId ?? 0);
                    reuniao.PublicadorOracao = _publicador.GetId(reuniao.PublicadorOracaoId ?? 0);

                    foreach (var parte in reuniao.Partes.Where(x => x.Numero != 0))
                    {
                        if (parte.PublicadorParte is PublicadorParte publicadorParte && publicadorParte.PublicadorId != 0)
                        {
                            publicadorParte.ParteId = parte.Id;

                            if (publicadorParte.PublicadorId != 0)
                            {
                                if (parte.PublicadorParteId == 0)
                                {
                                    _publicadorParte.Add(publicadorParte);
                                    await _publicadorParte.SaveAsync();
                                }
                                else
                                    await _publicadorParte.UpdateAsync(publicadorParte);
                            }
                            parte.PublicadorParteId = publicadorParte.Id;
                            await _parte.UpdateAsync(parte);
                        }
                    }
                    await _reuniao.UpdateAsync(reuniao);
                    TempData["ErrorMessage"] = "Registro salvo com sucesso!";
                }
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
    }
}
