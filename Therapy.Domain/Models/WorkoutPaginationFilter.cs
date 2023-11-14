using Therapy.Domain.Models;

public class WorkoutPaginationFilter : PaginationFilter
{
    public bool includeMedia { get; set; }
}