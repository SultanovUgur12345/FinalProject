using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedSettingTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Settings",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Settings",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
