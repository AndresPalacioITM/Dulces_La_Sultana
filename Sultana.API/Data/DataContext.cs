using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sultana.Shared.Entities;

namespace Sultana.API.Data
{
    public class DataContext : IdentityDbContext<Empleado>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {



        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Produccion> Producciones { get; set; }

        public DbSet<Materia_Prima> Materias_Primas { get; set; }

        public DbSet<Producto_Venta> Productos_Ventas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Sellado> Sellados { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
