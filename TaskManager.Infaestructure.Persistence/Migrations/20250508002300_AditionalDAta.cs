using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infaestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AditionalDAta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AditionalData",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AditionalData",
                table: "Tasks");
        }
    }
}
