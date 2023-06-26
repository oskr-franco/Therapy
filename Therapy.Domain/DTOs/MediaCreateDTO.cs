using System.ComponentModel.DataAnnotations;

namespace Therapy.Domain.DTOs {
  public class MediaCreateDTO
  {
      public int? Id { get; set; }
      public string Url { get; set; }
      [RegularExpression("Image|Video")]
      public string Type { get; set; }
  }
}