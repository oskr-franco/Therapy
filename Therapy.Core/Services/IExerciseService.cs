using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;

namespace Therapy.Core.Services
{
  public interface IExerciseService
  {
      Task<ExerciseDto> GetByIdAsync(int id);
      Task<IEnumerable<Exercise>> GetAllAsync();
      Task AddAsync(ExerciseDto exercise);
      Task UpdateAsync(ExerciseDto exercise);
      Task DeleteAsync(int id);
      Task DeleteAsync(ExerciseDto exercise);
  }
}
