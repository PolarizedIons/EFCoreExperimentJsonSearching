using System;
using EFCoreExperiment.JsonSearching;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreExperiment.JsonSearching.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Translations = table.Column<Translations>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyEntities");
        }
    }
}
