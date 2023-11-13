namespace Therapy.Domain.DTOs.WorkoutExercise
{
    public class WorkoutExerciseUpdateDTO
    {
        public int? ExerciseId { get; set; }
        public Int16 Sets { get; set; }
        public Int16 Reps { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}