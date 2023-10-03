using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NevulaForo.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_father_comment",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_id_father_comment",
                table: "Comment",
                column: "id_father_comment");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_id_father_comment",
                table: "Comment",
                column: "id_father_comment",
                principalTable: "Comment",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_id_father_comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_id_father_comment",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "id_father_comment",
                table: "Comment");
        }
    }
}
