using System.ComponentModel.DataAnnotations;
using Therapy.Domain.Validation;

namespace Therapy.Domain.DTOs.Media {
  public class MediaCreateDTO
  {
      [Required]
      [RegexDictionary("ImageUrl", "VideoUrl", "YoutubeUrl")]
      public string Url { get; set; }
      [RegularExpression("Image|Video")]
      public string Type { get; set; }
  }
}