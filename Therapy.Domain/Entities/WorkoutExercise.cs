namespace Therapy.Domain.Entities
{
  public class WorkoutExercise
  {
    public int WorkoutId { get; set; }
    public virtual Workout Workout { get; set; }

    public int ExerciseId { get; set; }
    public virtual Exercise Exercise { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public TimeSpan? Duration { get; set; }
  }
}