using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderWorkoutExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Order",
                table: "WorkoutExercises",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "WorkoutExercises");
        }
    }
}
