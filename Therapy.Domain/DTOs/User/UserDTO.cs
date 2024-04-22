using Therapy.Domain.Entities;

namespace Therapy.Domain.DTOs.User
{
  public class UserDTO
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual RefreshToken RefreshToken { get; set; }
  }
}