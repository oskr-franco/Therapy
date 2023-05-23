namespace Therapy.Domain.Entities
{
  public class Exercise
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public string Instructions { get; set; }
      public ICollection<Media> Media { get; set; }
  }
}