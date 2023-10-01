using Therapy.Domain.DTOs.Workout;
using Therapy.Domain.Models;

namespace Therapy.Core.Services.Workouts {
  public interface IWorkoutService {
    Task<WorkoutDTO> GetByIdAsync(int id);
    Task<PaginationResponse<WorkoutDTO>>  GetAllAsync(PaginationFilter filter);
    Task<WorkoutDTO> AddAsync(WorkoutCreateDTO workout);
    Task UpdateAsync(int id, WorkoutUpdateDTO workout);
    Task DeleteAsync(int id);
  }
}