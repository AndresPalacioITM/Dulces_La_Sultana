using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sultana.Shared.Entities;

namespace Sultana.API.Data
{
    public class DataContext : IdentityDbContext
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
        
        

       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
