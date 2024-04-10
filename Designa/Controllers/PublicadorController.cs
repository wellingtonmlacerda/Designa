using Designa.DAL;
using Designa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace Designa.Controllers
{
    public class PublicadorController : Controller
    {
        private readonly IGenericRepository<Publicador> _publicador;
        public PublicadorController(IGenericRepository<Publicador> publicador)
        {
            _publicador = publicador;
        }
        public async Task<IActionResult> Index(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            await CarregaPaisAsync();
            var publicador = await _publicador.GetAllWithIncludes(p => p.Pai, m => m.Mae);
            return View(publicador.OrderBy(o => o.Nome).ToPagedList(numeroPagina, 15));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Status,Observacao,Sexo,EMenorIdade,PaiId,MaeId")] Publicador publicador)
        {
            await CarregaPaisAsync();

            if (ModelState.IsValid)
            {
                _publicador.Add(publicador);
                await _publicador.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await CarregaPaisAsync(id);

                if (id != 0)
                {
                    if (await _publicador.GetIdWithIncludesAsync(id, p => p.Pai, m => m.Mae) is Publicador publicador)
                        return PartialView("_Edit", publicador);
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
        public async Task<IActionResult> Update([Bind("Id,Nome,Status,Observacao,Sexo,EMenorIdade,PaiId,MaeId")] Publicador publicador)
        {
            try
            {
                await CarregaPaisAsync();

                if (publicador.Id != 0 && ModelState.IsValid)
                {
                    await _publicador.UpdateAsync(publicador);
                    return RedirectToAction(nameof(Index));
                }
                return View(publicador);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await CarregaPaisAsync();
            var publicador = await _publicador.GetIdAsync(id);
            if (publicador != null)
            {
                await _publicador.RemoveAsync(publicador);
            }

            await _publicador.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Pesquisa(string Nome)
        {
            try
            {
                ViewBag.Nome = Nome;
                var publicador = await _publicador.GetListAsync(x => x.Nome.Contains(Nome));
                if (publicador.Count() > 0)
                {
                    return View("Index", publicador);
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
