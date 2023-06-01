using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstructionsLength2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Exercises",
                type: "VARCHAR(8000)",
                maxLength: 8000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Exercises",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(8000)",
                oldMaxLength: 8000);
        }
    }
}
