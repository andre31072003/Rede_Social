using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabalhoLab.Data;
using TrabalhoLab.Models;

namespace TrabalhoLab.Controllers
{
    public class PerfilsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PerfilsController(ApplicationDbContext context, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _dbContext=dbContext;
            _userManager=userManager;
        }

        // GET: Perfils
        public async Task<IActionResult> Index()
        {

            string currentUserName = User.Identity.Name;

            // Retrieve the single Perfil object for the logged-in user
            Perfil userPerfil = _dbContext.Perfil.FirstOrDefault(p => p.UserName == currentUserName);

            // Pass the Perfil object to the view
            return View(userPerfil);



            //public async Task<IActionResult> Index()
            //{
            //    var userName = User.Identity.Name;
            //    var userProfile = await _context.Perfils.FirstOrDefaultAsync(p => p.UserName == userName);
            //    var userPosts = await _context.Posts.Where(p => p.NomeCriadorPost == userName).ToListAsync();

            //    var viewModel = new UserPostsViewModel
            //    {
            //        UserProfile = userProfile,
            //        UserPosts = userPosts
            //    };

            //    return View(viewModel);
            //}

            //return _context.Perfil != null ? 
            //              View(await _context.Perfil.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Perfil'  is null.");
        }


        // GET: Perfils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Perfil == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfil
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // GET: Perfils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Nome,DataNascimento,Sexo,FotoPerfil,DataCriação")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: Perfils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Perfil == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfil.FindAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        // POST: Perfils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Nome,DataNascimento,Sexo,FotoPerfil")] Perfil perfil, IFormFile uploadedFile)
        {
            if (id != perfil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(perfil.Id.ToString());

                    if (user == null)
                    {
                        return NotFound($"Unable to load user with ID '{perfil.Id}'.");
                    }

                    var userNameExists = await _userManager.FindByNameAsync(perfil.UserName);
                    if (int.TryParse(user.Id, out var userIdAsInt) && userIdAsInt != perfil.Id)
                    {
                        ModelState.AddModelError(string.Empty, "Username already taken.");
                        return View(perfil);
                    }

                    user.UserName = perfil.UserName;
                    
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(perfil);
                    }

                    // Continue with the rest of your update logic, including profile picture handling
                    // ...

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilExists(perfil.Id))
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
            return View(perfil);
        }


        // GET: Perfils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Perfil == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfil
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // POST: Perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Perfil == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Perfil'  is null.");
            }
            var perfil = await _context.Perfil.FindAsync(id);
            if (perfil != null)
            {
                _context.Perfil.Remove(perfil);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilExists(int id)
        {
          return (_context.Perfil?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
