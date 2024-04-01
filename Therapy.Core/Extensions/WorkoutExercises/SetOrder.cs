using Therapy.Domain.Entities;

namespace Therapy.Core.Extensions.WorkoutExercises
{
    public static class OrderExtension
    {
        public static void SetOrder(this ICollection<WorkoutExercise> workoutExercises)
        {
            short order = 0;
            foreach (var workoutExercise in workoutExercises)
            {
                workoutExercise.Order = order;
                order++;
            }
        }
    }
}