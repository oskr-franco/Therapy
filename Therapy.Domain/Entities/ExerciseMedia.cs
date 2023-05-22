namespace Therapy.Domain.Entities {
  public class ExerciseMedia
  {
      public int Id { get; set; }
      public string Url { get; set; }
      public MediaType Type { get; set; }
      public string Description { get; set; }
      public Exercise Exercise { get; set; }
      public int ExerciseId { get; set; }
  }
  public enum MediaType
  {
      Image,
      Video
  }
}