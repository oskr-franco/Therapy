using Therapy.Domain.DTOs.Exercise;

namespace Therapy.Domain.DTOs.WorkoutExercise
{
    public class WorkoutExerciseDTO
    {
        public int ExerciseId { get; set; }
        public ExerciseDTO Exercise { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}