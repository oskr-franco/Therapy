using System.ComponentModel.DataAnnotations;

namespace Therapy.Domain.DTOs.WorkoutExercise
{
    public class WorkoutExerciseCreateDTO
    {
        public int? ExerciseId { get; set; }
        [Range(1, 100)]
        public Int16 Sets { get; set; }
        [Range(0, 100)]
        public Int16 Reps { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}