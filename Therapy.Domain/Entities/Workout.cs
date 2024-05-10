using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Therapy.Domain.Entities
{
  public class Workout: BaseWithCreatedBy
  {
    [Column(TypeName = "VARCHAR")]
    [MaxLength(200)]
    public string Name { get; set; }
    public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
  }
}
