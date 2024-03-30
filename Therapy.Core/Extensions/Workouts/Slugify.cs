using Therapy.Core.Utils;
using Therapy.Domain.Entities;

namespace Therapy.Core.Extensions.Workouts
{
    public static class SlugifyExtension
    {
        public static string GetSlug(this Workout workout)
        {
            return SlugConverter.GenerateSlug(workout.Id, workout.Name);
        }
    }
}