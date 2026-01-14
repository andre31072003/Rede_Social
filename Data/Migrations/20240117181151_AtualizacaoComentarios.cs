using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoComentarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "PertenceGrupos");

            migrationBuilder.AddColumn<int>(
                name: "PostsPostId",
                table: "Comentarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PostsPostId",
                table: "Comentarios",
                column: "PostsPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Posts_PostsPostId",
                table: "Comentarios",
                column: "PostsPostId",
                principalTable: "Posts",
                principalColumn: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Posts_PostsPostId",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_PostsPostId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "PostsPostId",
                table: "Comentarios");

            migrationBuilder.CreateTable(
                name: "PertenceGrupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAdesão = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    NomeUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PertenceGrupos", x => x.Id);
                });
        }
    }
}
