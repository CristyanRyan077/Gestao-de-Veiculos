using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Gestao_de_combustivel.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VeiculoUsuarios>()
                .HasKey(vu => new { vu.VeiculoId, vu.UsuarioId });
            modelBuilder.Entity<VeiculoUsuarios>()
                .HasOne(vu => vu.Veiculo).WithMany(v => v.Usuarios)
            .HasForeignKey(vu => vu.VeiculoId);
            modelBuilder.Entity<VeiculoUsuarios>()
                .HasOne(vu => vu.Usuario).WithMany(u => u.Veiculos)
            .HasForeignKey(vu => vu.UsuarioId);


        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<VeiculoUsuarios> VeiculoUsuarios { get; set; }
    }
}
