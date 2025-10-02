using Microsoft.EntityFrameworkCore;
using ERP.API.Models;

namespace ERP.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Solo las tablas que vas a usar
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeo de tablas
            modelBuilder.Entity<Rol>().ToTable("roles");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Sesion>().ToTable("sesiones");

            // Relaciones entre las entidades
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)               
                .WithMany(r => r.Usuarios)        
                .HasForeignKey(u => u.RolId);     

            modelBuilder.Entity<Sesion>()
                .HasOne(s => s.Usuario)           
                .WithMany(u => u.Sesiones)        
                .HasForeignKey(s => s.UsuarioId); // FK en la tabla sesiones
        }
    }
}

