using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameCintesssToCines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cintes",
                table: "Cintes");

            migrationBuilder.RenameTable(
                name: "Cintes",
                newName: "Cines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cines",
                table: "Cines",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cines",
                table: "Cines");

            migrationBuilder.RenameTable(
                name: "Cines",
                newName: "Cintes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cintes",
                table: "Cintes",
                column: "Id");
        }
    }
}
