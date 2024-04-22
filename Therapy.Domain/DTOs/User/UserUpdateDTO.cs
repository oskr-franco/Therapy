using System.ComponentModel.DataAnnotations;
using Therapy.Domain.Entities;

namespace Therapy.Domain.DTOs.User
{
  public class UserUpdateDTO
  {
    public int Id { get; set; }
    [MaxLength(320)]
    [RegularExpression(@"(^$|^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)", ErrorMessage = "Email is not valid")]
    public string Email { get; set; }
    [MinLength(10)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? UserTypeId { get; set; }
    public virtual RefreshToken RefreshToken { get; set; }
  }
}