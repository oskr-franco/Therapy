using Therapy.Domain.DTOs.Workout;

namespace Therapy.Core.Services.Workouts {
  public interface IWorkoutService {
    Task<WorkoutDTO> GetByIdAsync(int id);
    Task<IEnumerable<WorkoutDTO>> GetAllAsync();
    Task<WorkoutDTO> AddAsync(WorkoutCreateDTO workout);
    Task UpdateAsync(int id, WorkoutUpdateDTO workout);
    Task DeleteAsync(int id);
  }
}