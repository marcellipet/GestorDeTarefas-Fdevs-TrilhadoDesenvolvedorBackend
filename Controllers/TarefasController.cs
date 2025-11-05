using GestorTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tarefas = await _context.Tarefas
                .OrderByDescending(t => t.DataCriacao)
                .ToListAsync();
            return View(tarefas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tarefas.Any(e => e.Id == tarefa.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null) return NotFound();
            return View(tarefa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
