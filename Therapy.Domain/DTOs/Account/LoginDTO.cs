using System.ComponentModel.DataAnnotations;

namespace Therapy.Domain.DTOs.Account {
  public class LoginDTO {
    [MaxLength(320)]
    [RegularExpression(@"(^$|^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)", ErrorMessage = "Email is not valid")]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  }
}