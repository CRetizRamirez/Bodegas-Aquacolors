using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bodegas.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodegasA",
                columns: table => new
                {
                    idBodega = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bodega = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bodega__5C0091B7A887274B", x => x.idBodega);
                });

            migrationBuilder.CreateTable(
                name: "StockAqua",
                columns: table => new
                {
                    idStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Articulo = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    idBodega = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Accion = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StockAqu__A4B76DE526326946", x => x.idStock);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosA",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Correo = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Rol = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3213E83F31A51043", x => x.idUsuario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodegasA");

            migrationBuilder.DropTable(
                name: "StockAqua");

            migrationBuilder.DropTable(
                name: "UsuariosA");
        }
    }
}
