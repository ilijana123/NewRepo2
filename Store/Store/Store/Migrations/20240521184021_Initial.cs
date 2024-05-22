using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avtor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nacionalnost = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumRagjanje = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avtor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kniga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Godina = table.Column<int>(type: "int", nullable: true),
                    BrStrani = table.Column<int>(type: "int", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Zanr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tirazh = table.Column<int>(type: "int", nullable: true),
                    Izadavac = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SlikaUrl = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kniga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvtorKniga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnigaId = table.Column<int>(type: "int", nullable: false),
                    AvtorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvtorKniga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvtorKniga_Avtor_AvtorId",
                        column: x => x.AvtorId,
                        principalTable: "Avtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvtorKniga_Kniga_KnigaId",
                        column: x => x.KnigaId,
                        principalTable: "Kniga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvtorKniga_AvtorId",
                table: "AvtorKniga",
                column: "AvtorId");

            migrationBuilder.CreateIndex(
                name: "IX_AvtorKniga_KnigaId",
                table: "AvtorKniga",
                column: "KnigaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvtorKniga");

            migrationBuilder.DropTable(
                name: "Avtor");

            migrationBuilder.DropTable(
                name: "Kniga");
        }
    }
}
