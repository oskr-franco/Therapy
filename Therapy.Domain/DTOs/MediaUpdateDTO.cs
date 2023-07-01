using System.ComponentModel.DataAnnotations;

namespace Therapy.Domain.DTOs {
  /// <summary>
  /// AutoMapper is ignoring MediaDTO.Id, that is why we created this class to add another mapping(check MappingProfile).
  /// </summary>
  public class MediaUpdateDTO
  { 
    public int? Id { get; set; }
    public string Url { get; set; }
    [RegularExpression("Image|Video")]
    public string Type { get; set; }
  }
}