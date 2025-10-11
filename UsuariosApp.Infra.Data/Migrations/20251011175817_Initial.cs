using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.ID);
                });
           
            migrationBuilder.InsertData(
             table: "Perfil",
             columns: new[] { "ID", "NOME" },
             values: new object[,]
             {
                    { Guid.NewGuid(), "USUARIO" },
                    { Guid.NewGuid(), "ADMINISTRADOR" }
             });


            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PERFIL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usuario_Perfil_PERFIL_ID",
                        column: x => x.PERFIL_ID,
                        principalTable: "Perfil",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perfil_NOME",
                table: "Perfil",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EMAIL",
                table: "Usuario",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NOME",
                table: "Usuario",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PERFIL_ID",
                table: "Usuario",
                column: "PERFIL_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Perfil");
        }
    }
}
