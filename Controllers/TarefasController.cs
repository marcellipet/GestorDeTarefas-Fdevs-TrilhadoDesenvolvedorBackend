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

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var tarefas = await _context.Tarefas
                .OrderByDescending(t => t.DataCriacao)
                .ToListAsync();

            return View(tarefas);
        }

        // GET - CRIAR
        public IActionResult Create()
        {
            return View();
        }

        // POST - CRIAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.DataCriacao = DateTime.Now;
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // MARCAR COMO CONCLUÍDA / NÃO CONCLUÍDA
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            tarefa.Concluida = !tarefa.Concluida;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET - CONFIRMAR DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        // POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
