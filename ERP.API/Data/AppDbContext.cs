using ERP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Usuario - Rol
            modelBuilder.Entity<Usuario>()
                .HasOne<Rol>()
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.id_roles);

            // Relación Usuario - Sesiones
            modelBuilder.Entity<Sesion>()
                .HasOne(s => s.Usuario)
                .WithMany(u => u.Sesiones)
                .HasForeignKey(s => s.id_usuario);

            base.OnModelCreating(modelBuilder);
        }
    }
}
