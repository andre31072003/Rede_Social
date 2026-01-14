using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrabalhoLab.Models;

namespace TrabalhoLab.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Comenta> Comentarios { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Notificações> Notificações { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Denuncia> Denuncias { get; set; }
    }
}