using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vm_manager.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrivateKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PrivateKeyString = table.Column<string>(type: "text", nullable: false),
                    PublicKeyString = table.Column<string>(type: "text", nullable: false),
                    Passphrase = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerAuthentications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrivateKeyId = table.Column<Guid>(type: "uuid", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerAuthentications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerAuthentications_PrivateKeys_PrivateKeyId",
                        column: x => x.PrivateKeyId,
                        principalTable: "PrivateKeys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerAuthentications_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerAuthentications_PrivateKeyId",
                table: "ServerAuthentications",
                column: "PrivateKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerAuthentications_ServerId",
                table: "ServerAuthentications",
                column: "ServerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerAuthentications");

            migrationBuilder.DropTable(
                name: "PrivateKeys");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
