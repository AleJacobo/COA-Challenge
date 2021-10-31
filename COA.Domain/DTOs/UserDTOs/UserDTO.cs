using System.ComponentModel.DataAnnotations;

namespace COA.Domain.DTOs.UserDTOs
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Phone { get; set; }
    }
}
