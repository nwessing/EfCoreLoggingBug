using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfSqlForeignKeyBug.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "access_code",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    code = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    parentAccessCodeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_code", x => x.id);
                    table.ForeignKey(
                        name: "FK_access_code_access_code_parentAccessCodeId",
                        column: x => x.parentAccessCodeId,
                        principalTable: "access_code",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    accessCodeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_access_code_accessCodeId",
                        column: x => x.accessCodeId,
                        principalTable: "access_code",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            var workingAccessCodeId = Guid.NewGuid().ToString().ToUpper();
            migrationBuilder.Sql($@"
                INSERT INTO access_code
                (id, code, email, parentAccessCodeId)
                VALUES
                ('{workingAccessCodeId}', '123', 'test@example.com', NULL)
            ");

            var nonWorkingAccessCodeId = Guid.NewGuid().ToString();
            migrationBuilder.Sql($@"
                INSERT INTO access_code
                (id, code, email, parentAccessCodeId)
                VALUES
                ('{nonWorkingAccessCodeId}', '456', 'test@example.com', NULL)
            ");

            migrationBuilder.CreateIndex(
                name: "IX_access_code_code",
                table: "access_code",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_access_code_parentAccessCodeId",
                table: "access_code",
                column: "parentAccessCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_user_accessCodeId",
                table: "user",
                column: "accessCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "access_code");
        }
    }
}
