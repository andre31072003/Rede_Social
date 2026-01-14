using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostsForDenuncias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroDenuncias",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "AutorSuspenso",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutorSuspenso",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "NumeroDenuncias",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
