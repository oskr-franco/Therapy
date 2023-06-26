using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;

namespace Therapy.Core.Services
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
