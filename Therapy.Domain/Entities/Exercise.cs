using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Therapy.Domain.Entities
{
  public class Exercise : BaseWithCreatedBy
  {
      [Column(TypeName = "VARCHAR")]
      [MaxLength(200)]
      public string Name { get; set; }
      [Column(TypeName = "VARCHAR")]
      [MaxLength(2000)]
      public string Description { get; set; }
      [Column(TypeName = "VARCHAR")]
      [MaxLength(8000)]
      public string? Instructions { get; set; }
      public ICollection<Media> Media { get; set; }
      public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
  }
}