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
    public class NotificaçõesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificaçõesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notificações
        public async Task<IActionResult> Index()
        {
              return _context.Notificações != null ? 
                          View(await _context.Notificações.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Notificações'  is null.");
        }

        //public async Task<IActionResult> AceitarPedido(int notificacaoId)
        //{
        //    var notificacao = await _context.Notificações.FindAsync(notificacaoId);
        //    if (notificacao == null)
        //    {
        //        return NotFound();
        //    }

        //    var membro = new PertenceGrupo
        //    {
        //        NomeUtilizador = notificacao.NomeSolicitador,
        //        GrupoId = notificacao.GrupoId
        //    };
        //    _context.PertenceGrupos.Add(membro);
        //    await _context.SaveChangesAsync();


        //    return RedirectToAction("Index"); 
        //}

        public async Task<IActionResult> RecusarPedido(int notificacaoId)
        {
            var notificacao = await _context.Notificações.FindAsync(notificacaoId);
            if (notificacao == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index"); 
        }


        // GET: Notificações/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notificações == null)
            {
                return NotFound();
            }

            var notificações = await _context.Notificações
                .FirstOrDefaultAsync(m => m.NotificacaoId == id);
            if (notificações == null)
            {
                return NotFound();
            }

            return View(notificações);
        }

        // GET: Notificações/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notificações/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificacaoId,Destinatario,Mensagem,Data,NomeSolicitador,GrupoId")] Notificações notificações)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificações);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notificações);
        }

        // GET: Notificações/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notificações == null)
            {
                return NotFound();
            }

            var notificações = await _context.Notificações.FindAsync(id);
            if (notificações == null)
            {
                return NotFound();
            }
            return View(notificações);
        }

        // POST: Notificações/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotificacaoId,Destinatario,Mensagem,Data,NomeSolicitador,GrupoId")] Notificações notificações)
        {
            if (id != notificações.NotificacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificações);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificaçõesExists(notificações.NotificacaoId))
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
            return View(notificações);
        }

        // GET: Notificações/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notificações == null)
            {
                return NotFound();
            }

            var notificações = await _context.Notificações
                .FirstOrDefaultAsync(m => m.NotificacaoId == id);
            if (notificações == null)
            {
                return NotFound();
            }

            return View(notificações);
        }

        // POST: Notificações/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notificações == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Notificações'  is null.");
            }
            var notificações = await _context.Notificações.FindAsync(id);
            if (notificações != null)
            {
                _context.Notificações.Remove(notificações);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificaçõesExists(int id)
        {
          return (_context.Notificações?.Any(e => e.NotificacaoId == id)).GetValueOrDefault();
        }
    }
}
