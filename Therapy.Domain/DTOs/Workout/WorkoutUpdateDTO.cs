using Therapy.Domain.DTOs.WorkoutExercise;

namespace Therapy.Domain.DTOs.Workout {
  public class WorkoutUpdateDTO {
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<WorkoutExerciseUpdateDTO>? WorkoutExercises { get; set; }
  }
}