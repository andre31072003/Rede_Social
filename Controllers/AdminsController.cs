using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoLab.Data;
using TrabalhoLab.Data.Migrations;
using TrabalhoLab.Models;
using TrabalhoLab.Services;

namespace TrabalhoLab.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Injete o ApplicationDbContext no construtor
        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> AnalisarDenuncia(int id)
        {
            var denuncia = await _context.Denuncias.FindAsync(id);
            

            var post = await _context.Posts.FindAsync(denuncia.PostId);


            var viewModel = new AnalisarDenunciaViewModel
            {
                Denuncia = denuncia,
                Post = post
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessarDecisaoDenuncia(int DenunciaId, string Decisao, string Acao = null, int? DiasSuspensao = null)
        {
            var denuncia = await _context.Denuncias.FindAsync(DenunciaId);
            if (denuncia == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(denuncia.PostId);
            if (post == null)
            {
                return NotFound();
            }

            if (Decisao == "Reprovado")
            {
                denuncia.Status = "Reprovado";
            }
            else if (Decisao == "Aprovado")
            {
                denuncia.Status = "Aprovado";

                switch (Acao)
                {
                    case "EliminarPost":
                        _context.Posts.Remove(post);
                        break;

                    case "SuspenderAutor":
                        var autor = await _context.Perfil.FindAsync(post.PostId);
                        if (autor != null && DiasSuspensao.HasValue)
                        {
                            autor.DataFimSuspensao = DateTime.Now.AddDays(DiasSuspensao.Value);
                        }
                        break;

                    case "DeletarConta":
                        var usuario = await _context.Perfil.FindAsync(post.PostId);
                        if (usuario != null)
                        {
                            _context.Perfil.Remove(usuario);
                        }
                        break;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ListarDenuncias");
        }





    }
}
