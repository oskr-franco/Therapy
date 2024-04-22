using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Therapy.Domain.Entities
{
  public class RefreshToken
  {
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR")]
    [MaxLength(44)]
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsActive => DateTime.UtcNow < Expires;
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}