using Microsoft.EntityFrameworkCore;
using WebApiMusica.Entidades;

namespace WebApiMusica
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Artista> Artistas { get; set; }

        public DbSet<Cancion> Canciones { get; set; }
    }
}
