using COA.Core.Interfaces;
using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA_Challenge.Controllers
{
    [ApiController]
    [Route("COA/[controller]")]
    public class UsersController : ControllerBase
    {
        #region Fields and Constructor
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }
        #endregion

        #region Documentation
        /// <summary>
        /// Endpoint para obtener todos los usuarios de la base de datos.
        /// </summary>
        /// <para></para>
        /// <remarks>
        /// Formato de la peticion: 
        /// <br>GET /https:// servidor/COA/Users</br>
        /// <br></br>
        /// Ejemplo de peticion:
        /// <br>GET/https://localhost:44380/COA/Users</br>
        /// </remarks>
        /// <para></para>
        /// <returns>
        /// Una lista de objetos UserDTO
        /// </returns>
        /// <response code="200">Solicitud realizada con exito</response>
        #endregion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersServices.GetAll();

            var response = new Result<IEnumerable<UserDTO>>();
            await response.Success(users);

            return Ok(response);
        }

        #region Documentation
        /// <summary>
        /// Endpoint para la obtencion de un usuario, identificado por Id
        /// </summary>
        /// <para></para>
        /// <remarks>
        /// Formato de la peticion: 
        /// <br>GET/https:// servidor/COA/Users/{Id}</br>
        /// <br></br>
        /// Ejemplo de peticion:
        /// <br>GET/https://localhost:44380/COA/Users/1</br>
        /// </remarks>
        /// <para></para>
        /// <param name="id">Id del usuario</param>
        /// <returns>Los datos del usuario con el Id en cuestion</returns>
        /// <response code="200">Solicitud realizada con exito</response>
        /// <response code="400">Solicitud no se pudo ejecutar, con mensaje de error</response> 
        #endregion
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _usersServices.GetById(id);

            var response = new Result<UserDTO>();

            if (user == null)
            {
                await response.Fail("No se ha encontrado el usuario");
                return BadRequest(response);
            }

            await response.Success(user);
            return Ok(user);
        }

        #region Documentation
        /// <summary>
        /// Agregar usuario a la Base de Datos
        /// </summary>
        /// <para></para>
        /// <remarks>
        /// Formato de la peticion: 
        /// <br>
        ///     
        ///     PUT/https: //servidor/COA/Users
        ///     
        ///     {
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "user@example.com",
        ///         "phone": 0
        ///     }       
        /// 
        /// </br>
        /// <br></br>
        /// Ejemplo de peticion:
        /// <br>
        ///     
        ///     PUT/https: //localhost:44380/COA/Users
        ///     
        ///     {
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "user@example.com",
        ///         "phone": 0
        ///     }
        /// 
        /// </br>
        /// </remarks>
        /// <para></para>
        /// <param name="userInsertDTO">Modelo DTO para el Insert. Datos requeridos son verificados con DataAnnotations</param>
        /// <returns>Los datos del usuario con el Id en cuestion</returns>
        /// <response code="200">Solicitud realizada con exito</response>
        /// <response code="400">Solicitud no se pudo ejecutar, con mensaje de error</response> 
        #endregion
        [HttpPut]
        public async Task<IActionResult> Insert([FromForm] UserInsertDTO userInsertDTO)
        {
            var response = new Result();

            if (!ModelState.IsValid)
            {
                await response.Fail("Datos incorrectos");
                return BadRequest(response);
            }
            await _usersServices.Insert(userInsertDTO);
            return Ok();
        }

        #region Documentation
        /// <summary>
        /// Modifica un usuario de la base de datos
        /// </summary>
        /// <para></para>
        /// <remarks>
        /// Formato de la peticion: 
        /// <br>
        ///     
        ///     POST/https: //servidor/COA/Users/{id}
        ///     
        ///     {
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "user@example.com",
        ///         "phone": 0
        ///     }       
        /// 
        /// </br>
        /// <br></br>
        /// Ejemplo de peticion:
        /// <br>
        ///     
        ///     POST/https: //localhost:44380/COA/Users/1
        ///     
        ///     {
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "user@example.com",
        ///         "phone": 0
        ///     }
        /// 
        /// </br>
        /// </remarks>
        /// <para></para>
        /// <param name="userUpdateDTO">Modelo DTO para el Update. Datos requeridos son verificados con DataAnnotations</param>
        /// <param name="id">Id del usuario a modificar</param>
        /// <returns>Los datos del usuario con el Id en cuestion</returns>
        /// <response code="200">Solicitud realizada con exito</response>
        /// <response code="400">Solicitud no se pudo ejecutar, con mensaje de error</response> 
        #endregion
        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDTO userUpdateDTO, int id)
        {
            var response = new Result();

            if (!ModelState.IsValid)
            {
                await response.Fail("Los datos ingresados son incorrectos");
                return BadRequest(response);
            }

            await _usersServices.Update(userUpdateDTO, id);
            return Ok();
        }

        #region Documentation
        /// <summary>
        /// Elimina a un usuario de la base de datos, con baja logica
        /// </summary>
        /// <para></para>
        /// <remarks>
        /// Formato de la peticion: 
        /// <br>DELETE/ https:// servidor/COA/Users/{Id}</br>
        /// <br></br>
        /// Ejemplo de peticion:
        /// <br>DELETE/ https://localhost:44380/COA/Users/1</br>
        /// </remarks>
        /// <para></para>
        /// <param name="id">Id del usuario</param>
        /// <returns>Los datos del usuario con el Id en cuestion</returns>
        /// <response code="200">Solicitud realizada con exito</response>
        #endregion
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersServices.Delete(id);
            return Ok();
        }
    }
}
