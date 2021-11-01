using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COA.Domain.DTOs.UserDTOs
{
    public class UserInsertDTO
    {
        #region DataAnnotations-FirstName
        [Required(ErrorMessage = "Nombre de usuario es necesario")]
        [StringLength(30, ErrorMessage = "Debe contener minimo 5 caracteres", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR (50)")]
        #endregion
        public string FirstName { get; set; }

        #region DataAnnotations-LastName
        [Required(ErrorMessage = "Nombre de usuario es necesario")]
        [StringLength(50, ErrorMessage = "Debe contener minimo 5 caracteres", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR (50)")]
        #endregion
        public string LastName { get; set; }

        #region DataAnnotations-Email
        [Required(ErrorMessage = "Correo electronico es necesario")]
        [StringLength(50, ErrorMessage = "Debe contener minimo 15 caracteres", MinimumLength = 15)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Debe ser un email valido")]
        [Column(TypeName = "VARCHAR (50)")]
        [EmailAddress]
        #endregion
        public string Email { get; set; }

        #region DataAnnotations-Phone
        [Required(ErrorMessage = "Numero de telefono es necesario")]
        [Column(TypeName = "INTEGER")]
        #endregion
        public int Phone { get; set; }
    }
}
