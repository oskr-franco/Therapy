namespace Therapy.Domain.DTOs.WorkoutExercise
{
    public class WorkoutExerciseUpdateDTO
    {
        public int? ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}