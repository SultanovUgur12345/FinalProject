using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedWorkerTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentImage",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentImage",
                table: "Workers");
        }
    }
}
