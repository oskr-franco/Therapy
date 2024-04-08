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

        /// <summary>
        /// Orders the workout exercises by the order property
        /// This function exists because we can not use the OrderBy from an Included collection in EF Core
        /// </summary>
        /// <param name="workoutExercises">The collection of workout exercises to order</param>
        /// <returns> An ordered collection of workout exercises</returns>
        public static IOrderedEnumerable<WorkoutExercise> Order(this ICollection<WorkoutExercise> workoutExercises)
        {
            return workoutExercises.OrderBy(we => we.Order);
        }
    }
}