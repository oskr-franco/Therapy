using Therapy.Domain.DTOs.Exercise;

namespace Therapy.Domain.DTOs.WorkoutExercise
{
    public class WorkoutExerciseDTO
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Instructions { get; set; }
        public Int16 Sets { get; set; }
        public Int16 Reps { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}