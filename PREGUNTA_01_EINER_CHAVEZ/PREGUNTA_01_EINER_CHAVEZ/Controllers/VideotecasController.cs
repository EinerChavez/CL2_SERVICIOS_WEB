using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PREGUNTA_01_EINER_CHAVEZ.Data;
using PREGUNTA_01_EINER_CHAVEZ.Models;

namespace PREGUNTA_01_EINER_CHAVEZ.Controllers
{
    public class VideotecasController : Controller
    {
        private readonly PREGUNTA_01_EINER_CHAVEZContext _context;
        private const int PageSize = 10; // Tamaño de la página

        public VideotecasController(PREGUNTA_01_EINER_CHAVEZContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder, int page = 1)
        {
            IQueryable<Videoteca> videotecas = _context.Videoteca;

            // Filtrar por valor de búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int yearValue)) // Si es un año
                {
                    videotecas = videotecas.Where(v => v.Fecha.HasValue && v.Fecha.Value.Year == yearValue);
                }
                else if (decimal.TryParse(searchString, out decimal ratingValue)) // Si es un rating (decimal)
                {
                    videotecas = videotecas.Where(v => v.Rating == ratingValue);
                }
                else // Si es un texto (busca por nombre o cualquier otro texto)
                {
                    videotecas = videotecas.Where(v => v.Nombre.Contains(searchString) || v.Fecha.ToString().Contains(searchString));
                }
            }

            // Ordenar por diferentes criterios
            switch (sortOrder)
            {
                case "Name":
                    videotecas = videotecas.OrderBy(v => v.Nombre);
                    break;
                case "Name_desc":
                    videotecas = videotecas.OrderByDescending(v => v.Nombre);
                    break;
                case "Type":
                    videotecas = videotecas.OrderBy(v => v.Tipo);
                    break;
                case "Type_desc":
                    videotecas = videotecas.OrderByDescending(v => v.Tipo);
                    break;
                case "View":
                    videotecas = videotecas.OrderBy(v => v.VecesVista);
                    break;
                case "View_desc":
                    videotecas = videotecas.OrderByDescending(v => v.VecesVista);
                    break;
                case "Rating":
                    videotecas = videotecas.OrderBy(v => v.Rating);
                    break;
                case "Rating_desc":
                    videotecas = videotecas.OrderByDescending(v => v.Rating);
                    break;
                case "Date":
                    videotecas = videotecas.OrderBy(v => v.Fecha);
                    break;
                case "Date_desc":
                    videotecas = videotecas.OrderByDescending(v => v.Fecha);
                    break;
                default:
                    videotecas = videotecas.OrderBy(v => v.Nombre);
                    break;
            }

            // Paginación
            var totalItems = await videotecas.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            var paginatedVideotecas = await videotecas.Skip((page - 1) * PageSize)
                                                       .Take(PageSize)
                                                       .ToListAsync();

            // Pasar datos a la vista
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewData["ViewSortParm"] = sortOrder == "View" ? "View_desc" : "View";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "Rating_desc" : "Rating";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(paginatedVideotecas);
        }



        // GET: Videotecas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoteca = await _context.Videoteca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoteca == null)
            {
                return NotFound();
            }

            return View(videoteca);
        }

        // GET: Videotecas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videotecas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Tipo,VecesVista,Rating,Fecha")] Videoteca videoteca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoteca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoteca);
        }

        // GET: Videotecas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoteca = await _context.Videoteca.FindAsync(id);
            if (videoteca == null)
            {
                return NotFound();
            }
            return View(videoteca);
        }

        // POST: Videotecas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Tipo,VecesVista,Rating,Fecha")] Videoteca videoteca)
        {
            if (id != videoteca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoteca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideotecaExists(videoteca.Id))
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
            return View(videoteca);
        }

        // GET: Videotecas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoteca = await _context.Videoteca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoteca == null)
            {
                return NotFound();
            }

            return View(videoteca);
        }

        // POST: Videotecas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoteca = await _context.Videoteca.FindAsync(id);
            if (videoteca != null)
            {
                _context.Videoteca.Remove(videoteca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideotecaExists(int id)
        {
            return _context.Videoteca.Any(e => e.Id == id);
        }
    }
}
