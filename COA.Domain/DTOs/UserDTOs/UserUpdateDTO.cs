using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COA.Domain.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        #region DataAnnotations-FirstName
        [StringLength(30, ErrorMessage = "Debe contener minimo 5 caracteres", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR (50)")]
        #endregion
        public string FirstName { get; set; }

        #region DataAnnotations-LastName
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
        [Column(TypeName = "INTEGER")]
        #endregion
        public int? Phone { get; set; }
    }
}
