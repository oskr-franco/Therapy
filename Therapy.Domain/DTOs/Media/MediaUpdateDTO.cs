using System.ComponentModel.DataAnnotations;
using Therapy.Domain.Validation;

namespace Therapy.Domain.DTOs.Media {
  /// <summary>
  /// AutoMapper is ignoring MediaDTO.Id, that is why we created this class to add another mapping(check MappingProfile).
  /// </summary>
  public class MediaUpdateDTO
  { 
    public int? Id { get; set; }
    [Required]
    [RegexDictionary("ImageUrl", "VideoUrl", "YoutubeUrl")]
    public string Url { get; set; }
    [RegularExpression("Image|Video")]
    public string Type { get; set; }
  }
}