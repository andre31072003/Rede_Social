using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabalhoLab.Data;
using TrabalhoLab.Data.Migrations;
using TrabalhoLab.Models;

namespace TrabalhoLab.Controllers
{
    public class GruposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GruposController(ApplicationDbContext context)
        {
            _context = context;
        }      

        // GET: Grupos
        public async Task<IActionResult> Index()
        {

            //var NomeUtilizador = User.Identity.Name;
            //var grupos = await _context.Grupos.ToListAsync();

            //var viewModel = grupos.Select(g => new Grupo
            //{
            //    // Copie as propriedades necessárias do seu grupo para o ViewModel
            //    GrupoId = g.GrupoId,
            //    NomeGrupo = g.NomeGrupo,
            //    Descricao = g.Descricao,
            //    TipoAcesso = g.TipoAcesso,
            //    NomeDoCriadorGrupo = g.NomeDoCriadorGrupo,
            //    // ...
            //    // Aqui você verifica se o usuário é membro do grupo
            //    eMembro = _context.PertenceGrupos.Any(mg => mg.GrupoId == g.GrupoId && mg.NomeUtilizador == NomeUtilizador)
            //}).ToList();

            //return View(viewModel);

            return _context.Grupos != null ?
                        View(await _context.Grupos.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Grupos'  is null.");
        }

        public async Task<IActionResult> VerGrupo(int idGrupo)
        {
            var nomeDoGrupo = await _context.Grupos
                                             .Where(g => g.GrupoId == idGrupo)
                                             .Select(g => g.NomeGrupo)
                                             .FirstOrDefaultAsync();

            if (nomeDoGrupo == null)
            {
                return NotFound();
            }

            var postsDoGrupo = await _context.Posts
                                             .Where(p => p.GrupoId == idGrupo)
                                             .ToListAsync();

            ViewBag.NomeDoGrupo = nomeDoGrupo;
            ViewBag.GrupoId = idGrupo;
            return View(postsDoGrupo);
        }


        //[HttpPost]
        //public async Task<IActionResult> AderirGrupo(int gruposId)
        //{
        //    var grupo = await _context.Grupos.FindAsync(gruposId);

        //    if (grupo == null)
        //    {
        //        return NotFound();
        //    }

        //    var MembrosDoGrupo = new PertenceGrupo
        //    {
        //        NomeUtilizador = User.Identity.Name,
        //        GrupoId = gruposId,
        //    };

        //    _context.PertenceGrupos.Add(MembrosDoGrupo);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public async Task<IActionResult> PedirAcesso(int gruposId)
        //{
        //    var grupo = await _context.Grupos.FindAsync(gruposId);

        //    if (grupo == null)
        //    {
        //        return NotFound();
        //    }

        //    var nomeCriadorGrupo = grupo.NomeDoCriadorGrupo;
        //    var nomeUtilizador = User.Identity.Name;

        //    var notificações = new Notificações
        //    {
        //        Data = DateTime.Now,
        //        GrupoId = gruposId,
        //        NomeSolicitador = User.Identity.Name,
        //        Destinatario = nomeCriadorGrupo,
        //        Mensagem = $"O usuário {nomeUtilizador} quer aderir ao grupo {grupo.NomeGrupo}."
        //    };

        //    _context.Notificações.Add(notificações);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        // GET: Grupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grupos == null)
            {
                return NotFound();
            }

            var grupos = await _context.Grupos
                .FirstOrDefaultAsync(m => m.GrupoId == id);
            if (grupos == null)
            {
                return NotFound();
            }
            ViewBag.NomeDoCriadorGrupo = grupos.NomeDoCriadorGrupo;
            return View(grupos);
        }

        // GET: Grupos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrupoId,NomeGrupo,Descricao,TipoAcesso,DataCriacao")] Models.Grupo grupos)
        {
            if (ModelState.IsValid)
            {
                grupos.NomeDoCriadorGrupo = User.Identity.Name;
                grupos.DataCriacao = DateTime.Now;
                
                _context.Add(grupos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            
            return View(grupos);
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grupos == null)
            {
                return NotFound();
            }

            var grupos = await _context.Grupos.FindAsync(id);
            if (grupos == null)
            {
                return NotFound();
            }
            return View(grupos);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrupoId,NomeGrupo,Descricao,TipoAcesso,DataCriacao,NomeDoCriadorGrupo")] Models.Grupo grupos)
        {
            if (id != grupos.GrupoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GruposExists(grupos.GrupoId))
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
            return View(grupos);
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grupos == null)
            {
                return NotFound();
            }

            var grupos = await _context.Grupos
                .FirstOrDefaultAsync(m => m.GrupoId == id);
            if (grupos == null)
            {
                return NotFound();
            }

            return View(grupos);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grupos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Grupos'  is null.");
            }
            var grupos = await _context.Grupos.FindAsync(id);
            if (grupos != null)
            {
                _context.Grupos.Remove(grupos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GruposExists(int id)
        {
          return (_context.Grupos?.Any(e => e.GrupoId == id)).GetValueOrDefault();
        }
    }
}
