using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace COA.Domain.Entities
{
    public abstract class EntityBase
    {

        #region AboutId
        /// <summary>
        /// Id que sera la Primary Key(PK) en la base de SQL
        /// Se autogenera con cada insert, asegurando la unanimidad de un Id unico por entrada
        /// <br></br>
        /// Tambien se autogenera la opcion de etiquetarlo como Identity
        /// <br></br>
        /// Todas las entidades deben de heredar de EntityBase
        /// </summary> 
        #endregion
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        #region AboutIsDeleted
        /// <summary>
        /// IsDeleted se usa para un borrado de baja logica, donde: 
        /// <para>
        /// false= se encuentra en la base de datos.
        /// <br></br>
        /// true= no se encuentra en la base de datos.
        /// </para>
        /// </summary>
        #endregion
        [JsonIgnore]
        public bool IsDeleted { get; set; }

        #region AboutCreatedAt
        /// <summary>
        /// CreatedAt proporcionara la fecha y hora de un registro, tanto de creacion como de modificacion.
        /// </summary>
        #endregion
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
