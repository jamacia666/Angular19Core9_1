using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasAPI.Migrations
{
    /// <inheritdoc />
    public partial class NuevaTablaActores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "foto",
                table: "Actores",
                newName: "Foto");

            migrationBuilder.RenameColumn(
                name: "fechaNacimiento",
                table: "Actores",
                newName: "FechaNacimiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "Actores",
                newName: "foto");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Actores",
                newName: "fechaNacimiento");
        }
    }
}
