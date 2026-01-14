using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class denuncias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Comentarios_Posts_PostsPostId",
            //    table: "Comentarios");

            //migrationBuilder.DropIndex(
            //    name: "IX_Comentarios_PostsPostId",
            //    table: "Comentarios");

            //migrationBuilder.DropColumn(
            //    name: "PostsPostId",
            //    table: "Comentarios");

            migrationBuilder.AddColumn<int>(
                name: "NumeroDenuncias",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Denuncias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denuncias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Denuncias_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Denuncias_PostId",
                table: "Denuncias",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Denuncias");

            migrationBuilder.DropColumn(
                name: "NumeroDenuncias",
                table: "Posts");

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
    }
}
