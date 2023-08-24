using Therapy.Domain.DTOs.Exercise;

namespace Therapy.Core.Services.Exercises
{
  public interface IExerciseService
  {
      Task<ExerciseDTO> GetByIdAsync(int id);
      Task<IEnumerable<ExerciseDTO>> GetAllAsync();
      Task<ExerciseDTO> AddAsync(ExerciseCreateDTO exercise);
      Task UpdateAsync(int id, ExerciseUpdateDTO exercise);
      Task DeleteAsync(int id);
  }
}
