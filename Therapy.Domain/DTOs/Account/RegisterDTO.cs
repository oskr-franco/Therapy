using System.ComponentModel.DataAnnotations;
using Therapy.Domain.DTOs.User;

namespace Therapy.Domain.DTOs.Account {
  public class RegisterDTO: UserCreateDTO {
    [Compare("Password")]
    [Required]
    public string ConfirmPassword { get; set; }
  }
}