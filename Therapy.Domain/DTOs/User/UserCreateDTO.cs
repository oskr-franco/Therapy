using System.ComponentModel.DataAnnotations;

namespace Therapy.Domain.DTOs.User
{
  public class UserCreateDTO
  {
    [MaxLength(320)]
    [RegularExpression(@"(^$|^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)", ErrorMessage = "Email is not valid")]
    [Required]
    public string Email { get; set; }
    [MinLength(10)]
    [Required]
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public int? UserTypeId { get; set; }
  }
}