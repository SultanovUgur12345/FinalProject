using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFeatureTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipSlider",
                table: "ShipSlider");

            migrationBuilder.RenameTable(
                name: "ShipSlider",
                newName: "ShipSliders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipSliders",
                table: "ShipSliders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipSliders",
                table: "ShipSliders");

            migrationBuilder.RenameTable(
                name: "ShipSliders",
                newName: "ShipSlider");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipSlider",
                table: "ShipSlider",
                column: "Id");
        }
    }
}
