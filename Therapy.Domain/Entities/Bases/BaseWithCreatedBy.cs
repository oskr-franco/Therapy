namespace Therapy.Domain.Entities
{
  public class BaseWithCreatedBy: BaseEntity
  {
    public int CreatedBy { get; set; }
    public User Creator { get; set; }
  }
}