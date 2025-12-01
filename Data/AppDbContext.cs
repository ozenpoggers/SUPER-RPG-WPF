using Microsoft.EntityFrameworkCore;
using SUPERRPGWPF.Models;

namespace SUPERRPGWPF.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Itens { get; set; }
        public DbSet<Personagem> Personagens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=rpg.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Personagem)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PersonagemId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ se remover personagem, remove itens juntos
        }
    }
}
