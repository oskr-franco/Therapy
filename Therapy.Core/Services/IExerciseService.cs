using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;

namespace Therapy.Core.Services
{
  public interface IExerciseService
  {
      Task<ExerciseDTO> GetByIdAsync(int id);
      Task<IEnumerable<ExerciseDTO>> GetAllAsync();
      Task<ExerciseDTO> AddAsync(ExerciseDTO exercise);
      Task UpdateAsync(int id, ExerciseDTO exercise);
      Task DeleteAsync(int id);
  }
}
