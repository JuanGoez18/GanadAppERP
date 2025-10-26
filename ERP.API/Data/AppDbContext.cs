using Microsoft.EntityFrameworkCore;
using ERP.API.Models;

namespace ERP.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolDTO> Roles { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
