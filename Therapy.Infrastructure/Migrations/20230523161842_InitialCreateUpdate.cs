using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMedia_Exercises_ExerciseId",
                table: "ExerciseMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseMedia",
                table: "ExerciseMedia");

            migrationBuilder.RenameTable(
                name: "ExerciseMedia",
                newName: "Media");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseMedia_ExerciseId",
                table: "Media",
                newName: "IX_Media_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Exercises_ExerciseId",
                table: "Media",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Exercises_ExerciseId",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "ExerciseMedia");

            migrationBuilder.RenameIndex(
                name: "IX_Media_ExerciseId",
                table: "ExerciseMedia",
                newName: "IX_ExerciseMedia_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseMedia",
                table: "ExerciseMedia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMedia_Exercises_ExerciseId",
                table: "ExerciseMedia",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
