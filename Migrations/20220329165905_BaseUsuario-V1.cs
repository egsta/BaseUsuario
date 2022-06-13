using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseUsuario.Migrations
{
    public partial class BaseUsuarioV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    txt_desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    txt_desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cant_disponibles_alquiler = table.Column<int>(type: "int", nullable: true),
                    cant_disponibles_venta = table.Column<int>(type: "int", nullable: true),
                    precio_alquiler = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    precio_venta = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SnActivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenerosPeliculas",
                columns: table => new
                {
                    CodGenerosId = table.Column<int>(type: "int", nullable: false),
                    IdsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerosPeliculas", x => new { x.CodGenerosId, x.IdsId });
                    table.ForeignKey(
                        name: "FK_GenerosPeliculas_Generos_CodGenerosId",
                        column: x => x.CodGenerosId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenerosPeliculas_Peliculas_IdsId",
                        column: x => x.IdsId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolsId = table.Column<int>(type: "int", nullable: false),
                    SnActivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Rols_RolsId",
                        column: x => x.RolsId,
                        principalTable: "Rols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlquilerVenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alq_com = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    devolucion = table.Column<int>(type: "int", nullable: true),
                    PeliculasId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlquilerVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlquilerVenta_Peliculas_PeliculasId",
                        column: x => x.PeliculasId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlquilerVenta_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlquilerVenta_PeliculasId",
                table: "AlquilerVenta",
                column: "PeliculasId");

            migrationBuilder.CreateIndex(
                name: "IX_AlquilerVenta_UsuariosId",
                table: "AlquilerVenta",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_GenerosPeliculas_IdsId",
                table: "GenerosPeliculas",
                column: "IdsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlquilerVenta");

            migrationBuilder.DropTable(
                name: "GenerosPeliculas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Rols");
        }
    }
}
