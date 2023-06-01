namespace Therapy.Domain.DTOs {
  /// <summary>
  /// AutoMapper is ignoring MediaDTO.Id, that is why we created this class to add another mapping(check MappingProfile).
  /// </summary>
  public class MediaUpdateDTO: MediaDTO
  { }
}