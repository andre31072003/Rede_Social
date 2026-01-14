using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabalhoLab.Data;
using TrabalhoLab.Models;

namespace TrabalhoLab.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Comentarios
        public async Task<IActionResult> Index(int idPost)
        {
        //    var comentariosDoPost = await _context.Comentarios
        //.Where(c => c.IdPost == idPost)
        //.ToListAsync();

        //    return View(comentariosDoPost);

            return _context.Comentarios != null ?
                        View(await _context.Comentarios.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Comentarios'  is null.");
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comentarios == null)
            {
                return NotFound();
            }

            var comenta = await _context.Comentarios
                .FirstOrDefaultAsync(m => m.IdComentário == id);
            if (comenta == null)
            {
                return NotFound();
            }

            return View(comenta);
        }

        // GET: Comentarios/Create
        //public IActionResult Create()
        //{

        //    return View();
        //}
        [HttpGet]
        public IActionResult Create(int idPost)
        {
            var comenta = new Comenta { IdPost = idPost };
            return View(comenta);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComentário,Comentário,DataComentario,IdPost")] Comenta comenta)
        {
            if (ModelState.IsValid)
            {
                comenta.NomeAutorComentário = User.Identity.Name;
                comenta.DataComentario = DateTime.Now;

                _context.Add(comenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comenta);
        }


        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comentarios == null)
            {
                return NotFound();
            }

            var comenta = await _context.Comentarios.FindAsync(id);
            if (comenta == null)
            {
                return NotFound();
            }
            return View(comenta);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComentário,NomeAutorComentário,Comentário,DataComentario,IdPost")] Comenta comenta)
        {
            if (id != comenta.IdComentário)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentaExists(comenta.IdComentário))
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
            return View(comenta);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comentarios == null)
            {
                return NotFound();
            }

            var comenta = await _context.Comentarios
                .FirstOrDefaultAsync(m => m.IdComentário == id);
            if (comenta == null)
            {
                return NotFound();
            }

            return View(comenta);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comentarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Comentarios'  is null.");
            }
            var comenta = await _context.Comentarios.FindAsync(id);
            if (comenta != null)
            {
                _context.Comentarios.Remove(comenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentaExists(int id)
        {
          return (_context.Comentarios?.Any(e => e.IdComentário == id)).GetValueOrDefault();
        }
    }
}
