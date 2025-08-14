using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestao_de_combustivel.Models;
using System.Diagnostics;

namespace Gestao_de_combustivel.Controllers
{
    public class ConsumosController : Controller
    {
        private readonly DataContext _context;

        public ConsumosController(DataContext context)
        {
            _context = context;
        }

        // GET: Consumos
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Consumos.Include(c => c.Veiculo);
            return View(await dataContext.ToListAsync());
        }

        // GET: Consumos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }

        // GET: Consumos/Create
        public IActionResult Create()
        {
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Marca");
            return View();
        }

        // POST: Consumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Data,Valor,Tipo,VeiculoId")] Consumo consumo)
        {
            Debug.WriteLine("=== POST Create ===");
            Debug.WriteLine($"Descricao: {consumo.Descricao}");
            Debug.WriteLine($"Data: {consumo.Data}");
            Debug.WriteLine($"Valor: {consumo.Valor}");
            Debug.WriteLine($"Tipo: {consumo.Tipo}");
            Debug.WriteLine($"VeiculoId: {consumo.VeiculoId}");
            Debug.WriteLine("ModelState keys:");
            foreach (var key in ModelState.Keys)
            {
                Debug.WriteLine(key);
                foreach (var error in ModelState[key].Errors)
                {
                    Debug.WriteLine("  " + error.ErrorMessage);
                }
            }
            // Erros do ModelState
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            Debug.WriteLine("ModelState errors: " + string.Join(", ", errors));

            if (ModelState.IsValid)
            {
                Debug.WriteLine("ModelState válido, adicionando ao contexto...");
                _context.Add(consumo);
                await _context.SaveChangesAsync();
                Debug.WriteLine("Salvo com sucesso!");
                return RedirectToAction(nameof(Index));
            }

            Debug.WriteLine("ModelState inválido, retornando view.");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Marca", consumo.VeiculoId);
            return View(consumo);
        
        }

        // GET: Consumos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo == null)
            {
                return NotFound();
            }
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Marca", consumo.VeiculoId);
            return View(consumo);
        }

        // POST: Consumos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Data,Valor,Tipo,VeiculoId")] Consumo consumo)
        {
            if (id != consumo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumoExists(consumo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Marca", consumo.VeiculoId);
            return View(consumo);
        }

        // GET: Consumos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }

        // POST: Consumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo != null)
            {
                _context.Consumos.Remove(consumo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumoExists(int id)
        {
            return _context.Consumos.Any(e => e.Id == id);
        }
    }
}
