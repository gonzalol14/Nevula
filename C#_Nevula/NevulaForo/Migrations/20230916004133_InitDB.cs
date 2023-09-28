using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NevulaForo.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    surname = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    username = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "varchar(1750)", unicode: false, maxLength: 1750, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.id);
                    table.ForeignKey(
                        name: "FK_publications_users",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_role = table.Column<int>(type: "int", nullable: false),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_roles_roles",
                        column: x => x.id_role,
                        principalTable: "role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_roles_users",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_publication = table.Column<int>(type: "int", nullable: false),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_publications",
                        column: x => x.id_publication,
                        principalTable: "Publication",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_comments_users",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_id_publication",
                table: "Comment",
                column: "id_publication");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_id_user",
                table: "Comment",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_id_user",
                table: "Publication",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_id_role",
                table: "UserRole",
                column: "id_role");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_id_user",
                table: "UserRole",
                column: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
