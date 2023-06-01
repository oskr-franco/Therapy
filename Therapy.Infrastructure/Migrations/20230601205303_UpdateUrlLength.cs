using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Media",
                type: "VARCHAR(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Media",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(4000)",
                oldMaxLength: 4000);
        }
    }
}
