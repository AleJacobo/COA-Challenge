using COA.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COA.Domain
{
    public class User : EntityBase
    {
        [Required]
        [Column(TypeName = "VARCHAR (255)")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR (255)")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR (255)")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "INTEGER")]
        [MaxLength(20)]
        public int Phone { get; set; }

    }
}
