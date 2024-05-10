using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Workouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_CreatedBy",
                table: "Workouts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CreatedBy",
                table: "Exercises",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Users_CreatedBy",
                table: "Exercises",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_CreatedBy",
                table: "Workouts",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Users_CreatedBy",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_CreatedBy",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_CreatedBy",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_CreatedBy",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Exercises");
        }
    }
}
