using Designa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System;
using X.PagedList;
using static Designa.Helpers.Enums;

namespace Designa.Controllers
{
    public class PublicadorController : Controller
    {
        private readonly IGenericRepository<Publicador> _publicador;
        private readonly IGenericRepository<PublicadorPrivilegio> _publicadorPrivilegio;
        private readonly IConfiguration _configuracao;
        private readonly int _itensToPage;
        public PublicadorController(IGenericRepository<Publicador> publicador, IConfiguration configuracao, IGenericRepository<PublicadorPrivilegio> publicadorPrivilegio)
        {
            _publicador = publicador;
            _configuracao = configuracao;
            int.TryParse(configuracao["ItensToPage"], out _itensToPage);
            _itensToPage = _itensToPage == 0 ? 15 : _itensToPage;
            _publicadorPrivilegio = publicadorPrivilegio;
        }
        public async Task<IActionResult> Index(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            await CarregaPaisAsync();
            var publicador = await _publicador.GetAllWithIncludes(p => p.Include(x => x.Pai).Include(y => y.Mae).Include(p => p.PublicadorPrivilegios));

            return View(publicador.OrderBy(o => o.Nome).ToPagedList(numeroPagina, _itensToPage));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publicador publicador)
        {
            try
            {
                await CarregaPaisAsync();

                if (ModelState.IsValid)
                {
                    _publicador.Add(publicador);
                    await _publicador.SaveAsync();
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
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await CarregaPaisAsync(id);

                if (id != 0)
                {
                    if (await _publicador.GetIdWithIncludesAsync(id, p => p.Include(x => x.Pai).Include(y => y.Mae).Include(pri => pri.PublicadorPrivilegios)) is Publicador publicador)
                    {
                        foreach (var item in publicador.PublicadorPrivilegios)
                        {
                            publicador.Privilegios.Add(item.Privilegio);
                        }
                        return PartialView("_Edit", publicador);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Publicador publicador)
        {
            try
            {
                await CarregaPaisAsync();

                if (publicador.Id != 0 && ModelState.IsValid)
                {
                    if (await _publicadorPrivilegio.GetListAsync(x => x.PublicadorId == publicador.Id) 
                        is IEnumerable<PublicadorPrivilegio> publicadorPrivilegio)
                    {
                        var publicadorPrivilegioBanco = publicadorPrivilegio.ToList();
                        foreach (var privilegio in publicador.Privilegios.Where(x => !publicadorPrivilegioBanco.Any(y => y.Privilegio == x)))
                        {
                            _publicadorPrivilegio.Add(new PublicadorPrivilegio() { Privilegio = privilegio, PublicadorId = publicador.Id });
                            await _publicadorPrivilegio.SaveAsync();
                        }


                        var removePrivilegio = _publicadorPrivilegio.CreateNewObjectList();
                        foreach (var item in publicadorPrivilegioBanco.Where(x => !publicador.Privilegios.Any(y => y == x.Privilegio)))
                        {
                            removePrivilegio.Add(item);
                        }
                        
                        removePrivilegio.ForEach(x => { 
                            _publicadorPrivilegio.RemoveAsync(x);  
                            publicador.PublicadorPrivilegios.Remove(x);
                        });

                    }
                    await _publicador.UpdateAsync(publicador);
                    TempData["ErrorMessage"] = "Registro atualizado com sucesso!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await CarregaPaisAsync();
            var publicador = await _publicador.GetIdAsync(id);
            if (publicador != null)
            {
                await _publicador.RemoveAsync(publicador);
                TempData["ErrorMessage"] = "Registro excluído com sucesso!";
            }

            await _publicador.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Pesquisa(string Nome)
        {
            try
            {
                ViewBag.Nome = Nome;
                var publicador = await _publicador.GetListAsync(x => x.Nome.ToUpper().Contains(Nome.Trim().ToUpper()));
                if (publicador.Count() > 0)
                {
                    return View("Index", publicador.OrderBy(o => o.Nome).ToPagedList(1, _itensToPage));
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        private async Task CarregaPaisAsync(int id = 0)
        {
            if (await _publicador.GetListAsync(m => m.Sexo == "F") is IEnumerable<Publicador> publicadoras && publicadoras.Count() > 0)
                ViewData["MaeId"] = new SelectList(publicadoras.Where(x => x.Id != id), "Id", "Nome");
            if (await _publicador.GetListAsync(m => m.Sexo == "M") is IEnumerable<Publicador> publicadores && publicadores.Count() > 0)
                ViewData["PaiId"] = new SelectList(publicadores.Where(x => x.Id != id), "Id", "Nome");
        }
    }
}
