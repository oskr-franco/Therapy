using Therapy.Domain.DTOs.WorkoutExercise;

namespace Therapy.Domain.DTOs.Workout {
  public class WorkoutCreateDTO
  {
    public string Name { get; set; }
    public ICollection<WorkoutExerciseCreateDTO> WorkoutExercises { get; set; }
  }
}