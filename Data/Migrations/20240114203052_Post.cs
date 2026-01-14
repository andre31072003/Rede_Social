using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class Post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCriadorPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoPublicacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPost = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
