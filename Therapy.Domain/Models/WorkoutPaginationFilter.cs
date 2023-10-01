using Therapy.Domain.Models;

public class WorkoutPaginationFilter : PaginationFilter
{
    public bool includeExerciseDetails { get; set; }
}