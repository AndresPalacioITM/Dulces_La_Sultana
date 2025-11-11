using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sultana.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Empresa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriaPrimas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StockMinimo = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaPrimas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoTerminados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoTerminados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NombreVendedor = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Empresa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VentaCabeceras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: false),
                    Factura = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaCabeceras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VentaDetalles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaCabeceraId = table.Column<int>(type: "int", nullable: false),
                    LoteProductoTerminadoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDetalles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumoMPs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenProduccionId = table.Column<int>(type: "int", nullable: false),
                    LoteMateriaPrimaId = table.Column<int>(type: "int", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: false),
                    CantidadUsada = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumoMPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumoMPs_Empleados_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenProducciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoTerminadoId = table.Column<int>(type: "int", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: false),
                    LoteProducto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    FechaVencimientoPT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadPeso = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenProducciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenProducciones_Empleados_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenProducciones_ProductoTerminados_ProductoTerminadoId",
                        column: x => x.ProductoTerminadoId,
                        principalTable: "ProductoTerminados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoteMateriaPrimas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaPrimaId = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: false),
                    NumeroLote = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    FechaHoraRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadIngresada = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    CantidadDisponible = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    InspeccionColor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InspeccionOlor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InspeccionTextura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EstadoEmpaque = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoteMateriaPrimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoteMateriaPrimas_Empleados_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoteMateriaPrimas_MateriaPrimas_MateriaPrimaId",
                        column: x => x.MateriaPrimaId,
                        principalTable: "MateriaPrimas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoteMateriaPrimas_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoteProductoTerminados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenProduccionId = table.Column<int>(type: "int", nullable: false),
                    StockPT = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    FechaVencimientoPT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoteProductoTerminados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoteProductoTerminados_OrdenProducciones_OrdenProduccionId",
                        column: x => x.OrdenProduccionId,
                        principalTable: "OrdenProducciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Nit",
                table: "Clientes",
                column: "Nit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoMPs_ResponsableId",
                table: "ConsumoMPs",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Nombre",
                table: "Empleados",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoteMateriaPrimas_MateriaPrimaId_NumeroLote",
                table: "LoteMateriaPrimas",
                columns: new[] { "MateriaPrimaId", "NumeroLote" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoteMateriaPrimas_ProveedorId",
                table: "LoteMateriaPrimas",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_LoteMateriaPrimas_ResponsableId",
                table: "LoteMateriaPrimas",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_LoteProductoTerminados_OrdenProduccionId",
                table: "LoteProductoTerminados",
                column: "OrdenProduccionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MateriaPrimas_Nombre",
                table: "MateriaPrimas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenProducciones_ProductoTerminadoId_LoteProducto",
                table: "OrdenProducciones",
                columns: new[] { "ProductoTerminadoId", "LoteProducto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenProducciones_ResponsableId",
                table: "OrdenProducciones",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoTerminados_Nombre",
                table: "ProductoTerminados",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_Nit",
                table: "Proveedores",
                column: "Nit",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "ConsumoMPs");

            migrationBuilder.DropTable(
                name: "LoteMateriaPrimas");

            migrationBuilder.DropTable(
                name: "LoteProductoTerminados");

            migrationBuilder.DropTable(
                name: "VentaCabeceras");

            migrationBuilder.DropTable(
                name: "VentaDetalles");

            migrationBuilder.DropTable(
                name: "MateriaPrimas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "OrdenProducciones");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "ProductoTerminados");
        }
    }
}
