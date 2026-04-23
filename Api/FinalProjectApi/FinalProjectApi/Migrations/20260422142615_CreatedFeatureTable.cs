using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFeatureTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
