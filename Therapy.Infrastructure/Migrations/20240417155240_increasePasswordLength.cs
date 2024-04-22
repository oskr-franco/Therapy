using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class increasePasswordLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "VARCHAR(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(74)",
                oldMaxLength: 74);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(320)",
                oldMaxLength: 320);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "VARCHAR(74)",
                maxLength: 74,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "VARCHAR(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);
        }
    }
}
