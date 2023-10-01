using Therapy.Domain.DTOs.Exercise;

namespace Therapy.Domain.DTOs.WorkoutExercise
{
    public class WorkoutExerciseDTO
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Instructions { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}