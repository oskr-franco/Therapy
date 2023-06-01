using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Therapy.Domain.Entities {
  public class Media
  {
      public int Id { get; set; }
      [Column(TypeName = "VARCHAR")]
      [MaxLength(4000)]
      public string Url { get; set; }
      public MediaType Type { get; set; }
      public Exercise Exercise { get; set; }
      public int ExerciseId { get; set; }
  }
  public enum MediaType
  {
      Image,
      Video
  }
}