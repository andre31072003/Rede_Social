using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TrabalhoLab.Data;
using TrabalhoLab.Data.Migrations;
using TrabalhoLab.Models;

namespace TrabalhoLab.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PostsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public InputModel Input { get; set; }

        public class InputModel
        {
            [NotMapped]
            public IFormFile? FotoPublicacao { get; set; }
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts.ToListAsync();
            // Ensure that 'posts' is a List of 'TrabalhoLab.Models.Posts'

            var comments = await _context.Comentarios.ToListAsync();
            // Ensure that 'comments' is a List of 'TrabalhoLab.Models.Comenta'

            var model = new Tuple<IEnumerable<TrabalhoLab.Models.Posts>, IEnumerable<TrabalhoLab.Models.Comenta>>(posts, comments);
            return View(model);


            //var posts = await _context.Posts
            //                  .Include(p => p.Comentas) // Make sure comments are included
            //                  .ToListAsync();
            //return View(posts);

            //return _context.Posts != null ?
            //            View(await _context.Posts.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Posts'  is null.");


        }

        //public async Task<IActionResult> Index()
        //{
        //    // Substitua "Publicacao" pelo nome correto do seu DbSet de publicações
        //    var publicacoesPublicas = await _context.Posts
        //                                    .Where(p => p.TipoPost == "Publico")
        //                                    .ToListAsync();

        //    return View(publicacoesPublicas);
        //}


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Posts/Create
        public IActionResult Create(int gruposId)
        {
            ViewBag.GrupoId = gruposId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Texto, NomeCriadorPost,TipoPost,GrupoId")] Posts posts, InputModel Input)
        {
            posts.NomeCriadorPost = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (Input.FotoPublicacao != null && Input.FotoPublicacao.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Publicações");
                    var uniqueFileName = $"{Guid.NewGuid()}_{Input.FotoPublicacao.FileName}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Input.FotoPublicacao.CopyToAsync(fileStream);
                    }

                    posts.FotoPublicacao = uniqueFileName; // Save just the filename or a relative path
                }

                posts.NomeCriadorPost = User.Identity.Name;

                _context.Add(posts);
                await _context.SaveChangesAsync();
                //return RedirectToAction("VerGrupo", new { idGrupo = posts.GrupoId });
                return RedirectToAction("VerGrupo", "Grupos", new { idGrupo = posts.GrupoId });
            }

            // If we got this far, something failed, redisplay form
            return View(posts);
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
            return RedirectToAction("VerGrupos", "Posts", new { idGrupo = ViewBag.GrupoId });

        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,NomeCriadorPost,Texto,FotoPublicacao,TipoPost,DataPost,GrupoId")] Posts posts)
        {
            if (id != posts.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.PostId))
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
            return View(posts);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var posts = await _context.Posts.FindAsync(id);
            if (posts != null)
            {
                _context.Posts.Remove(posts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
     

    }
}
