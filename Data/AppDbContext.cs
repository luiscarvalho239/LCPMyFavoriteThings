using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFavoriteThings.Data.Models;

namespace MyFavoriteThings.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Série> Séries { get; set; }
        public DbSet<Música> Músicas { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Tutorial> Tutoriais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>()
                .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());
        }
    }
}
