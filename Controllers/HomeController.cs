using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrabalhoLab.Data;
using TrabalhoLab.Data.Migrations;
using TrabalhoLab.Models;

namespace TrabalhoLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList(); // Replace with your actual code to get posts
            if (posts == null)
            {
                // Handle the case where there are no posts, or this was not supposed to happen
                // You could initialize an empty list to avoid null
                posts = new List<TrabalhoLab.Models.Posts>();
            }
            return View(posts);

            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult FeedSemConta()
        {
            var posts = _context.Posts.ToList(); // Replace with your actual code to get posts
            if (posts == null)
            {
                // Handle the case where there are no posts, or this was not supposed to happen
                // You could initialize an empty list to avoid null
                posts = new List<TrabalhoLab.Models.Posts>();
            }
            return View(posts);
            //return View();
        }

       
        public IActionResult Denunciar(int PostId)
        {
            var post = new Denuncia { PostId= PostId };
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Denunciar([Bind("PostId, Motivo")] Denuncia denuncia)
        {
            denuncia.Status = "Pendente";


                var post = await _context.Posts.FindAsync(denuncia.PostId);

                if (post != null)
                {


                    // Redirecionamento após salvar a denúncia
                    return RedirectToAction("VerGrupo", "Grupos", new { idGrupo = post.GrupoId });
                }
                else
                {
                    // Se o post não for encontrado, retorna um erro
                    return NotFound();
                }


            _context.Add(denuncia);
            await _context.SaveChangesAsync();
            return View(denuncia);
        }
        [HttpPost]
        public async Task<IActionResult> DenunciarPost(int postId, string motivo)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            var denuncia = new Denuncia
            {
                PostId = postId,
                Motivo = motivo,
                Status = "Pendente"
            };

            _context.Denuncias.Add(denuncia);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Ou outra view conforme necessário
        }


        public async Task<IActionResult> ListarDenuncias()
        {
            var denuncias = await _context.Denuncias.ToListAsync();
            return View(denuncias);
            

        }
        public IActionResult AnalisarDenuncia(int id)
        {
            
            return View();

        }

    }
}