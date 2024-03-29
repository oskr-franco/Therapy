using System.ComponentModel.DataAnnotations;

namespace Therapy.Domain.DTOs.Media {
  public class MediaDTO
  {
      public int Id { get; set; }
      public string Url { get; set; }
      [RegularExpression("Image|Video")]
      public string Type { get; set; }
  }
}