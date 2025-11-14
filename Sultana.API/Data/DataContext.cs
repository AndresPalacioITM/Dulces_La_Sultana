using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sultana.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Sultana.API.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<LoteMateriaPrima> LoteMateriaPrimas { get; set; }
        public DbSet<LoteProductoTerminado> LoteProductoTerminados { get; set; }
        public DbSet<MateriaPrima> MateriaPrimas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ProductoTerminado> ProductoTerminados { get; set; }
        public DbSet<OrdenProduccion> OrdenProducciones { get; set; }
        public DbSet<ConsumoMP> ConsumoMPs { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<VentaCabecera> VentaCabeceras { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }  
        
        public DbSet<Alerta> Alertas { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>().HasIndex(e => e.Nombre).IsUnique();
            modelBuilder.Entity<Proveedor>().HasIndex(p => p.Nit).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(c => c.Nit).IsUnique();
            modelBuilder.Entity<MateriaPrima>().HasIndex(m => m.Nombre).IsUnique();
            modelBuilder.Entity<ProductoTerminado>().HasIndex(p => p.Nombre).IsUnique();
            modelBuilder.Entity<LoteMateriaPrima>().HasIndex(l => new { l.MateriaPrimaId, l.NumeroLote }).IsUnique();
            modelBuilder.Entity<OrdenProduccion>().HasIndex(o => new { o.ProductoTerminadoId, o.LoteProducto }).IsUnique();
            modelBuilder.Entity<LoteProductoTerminado>().HasIndex(l => l.OrdenProduccionId).IsUnique();

            foreach (var p in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties().Where(p => p.ClrType == typeof(decimal))))
            {
                p.SetPrecision(18);
                p.SetScale(6);
            }

        }
    }
}
