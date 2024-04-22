using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Therapy.Domain.Entities
{
    public class UserType
    {
      public int Id { get; set; }
      [Column(TypeName = "VARCHAR")]
      [MaxLength(50)]
      public string TypeName { get; set; }
      public virtual List<User> Users { get; set; }
    }
}