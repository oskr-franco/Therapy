using Therapy.Domain.DTOs.Exercise;
using Therapy.Domain.Models;

namespace Therapy.Core.Services.Exercises
{
  public interface IExerciseService
  {
      Task<ExerciseDTO> GetByIdAsync(int id);
      Task<PaginationResponse<ExerciseDTO>> GetAllAsync(PaginationFilter filter);
      Task<PaginationResponse<ExerciseDTO>> GetByUserIdAsync(int userId, PaginationFilter filter);
      Task<ExerciseDTO> AddAsync(ExerciseCreateDTO exercise);
      Task UpdateAsync(int id, ExerciseUpdateDTO exercise);
      Task DeleteAsync(int id);
  }
}
