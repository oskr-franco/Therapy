using Microsoft.EntityFrameworkCore;
using Therapy.Domain.Entities;
using Therapy.Infrastructure.Repositories;

namespace Therapy.Core.Extensions
{
  public static class WorkoutPaginationExtension {
    public static IQueryable<Workout>  AsQueryableIncludeByFilter(
      this IRepository<Workout> repository,
      WorkoutPaginationFilter filter)
    {
      if (filter.includeExerciseDetails)
      {
        return repository.AsQueryable(include: e => e.Include(x => x.WorkoutExercises).ThenInclude(we => we.Exercise));
      }
      return repository.AsQueryable(include: e => e.Include(x => x.WorkoutExercises));
    }
  }
}