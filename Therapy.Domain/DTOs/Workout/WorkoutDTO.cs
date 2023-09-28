using System.ComponentModel.DataAnnotations;
using Therapy.Domain.DTOs.WorkoutExercise;

namespace Therapy.Domain.DTOs.Workout {
  public class WorkoutDTO
  {
    public int Id { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<WorkoutExerciseDTO> WorkoutExercises { get; set; }
  }
}