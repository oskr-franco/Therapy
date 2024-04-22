using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Therapy.Domain.Entities
{
  public class User : BaseEntity
  {
    [Column(TypeName = "VARCHAR")]
    [MaxLength(200)]
    public string Email { get; set; }
    [Column(TypeName = "VARCHAR")]
    [MaxLength(80)]
    public string Password { get; set; }
    [Column(TypeName = "VARCHAR")]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Column(TypeName = "VARCHAR")]
    [MaxLength(50)]
    public string LastName { get; set; }
    public virtual RefreshToken RefreshToken { get; set; }
    public virtual UserType? UserType { get; set; }
    public int? UserTypeId { get; set; } 
  }
}