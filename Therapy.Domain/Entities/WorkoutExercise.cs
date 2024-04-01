namespace Therapy.Domain.Entities
{
  public class WorkoutExercise
  {
    public int WorkoutId { get; set; }
    public virtual Workout Workout { get; set; }

    public int ExerciseId { get; set; }
    public virtual Exercise Exercise { get; set; }
    public Int16 Sets { get; set; }
    public Int16 Reps { get; set; }
    public TimeSpan? Duration { get; set; }
    public Int16? Order { get; set; }
  }
}