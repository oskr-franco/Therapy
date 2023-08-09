using System.ComponentModel.DataAnnotations;
using Therapy.Domain.Validation;

namespace Therapy.Domain.DTOs {
  public class MediaCreateDTO
  {
      public int? Id { get; set; }
      [Required]
      [RegexDictionary("ImageUrl", "VideoUrl", "YoutubeUrl")]
      public string Url { get; set; }
      [RegularExpression("Image|Video")]
      public string Type { get; set; }
  }
}