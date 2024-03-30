using Therapy.Core.Utils;
using Therapy.Domain.Entities;

namespace Therapy.Core.Extensions.Exercises
{
    public static class SlugifyExtension
    {
        public static string GetSlug(this Exercise exercise)
        {
            return SlugConverter.GenerateSlug(exercise.Id, exercise.Name);
        }
    }
}