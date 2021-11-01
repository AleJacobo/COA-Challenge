using COA.Core.Interfaces;
using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA_Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        #region Fields and Constructor
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }
        #endregion

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            try
            {
                var request = await _usersServices.GetAll();

                if (request == null)
                    return BadRequest(new Result().Fail("No se ha podido encontrar entradas en la Base de Datos"));

                return Ok(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No se ha podido ejecutar la operacion.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            try
            {
                var request = await _usersServices.GetById(id);

                if (request == null)
                    return BadRequest(new Result().Fail("No se ha encontrado un usuario con este Id"));

                return Ok(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No se ha podido ejecutar la operacion.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Result>> Insert([FromForm] UserInsertDTO userInsertDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new Result().Fail("Datos incorrectos"));

                var request = await _usersServices.Insert(userInsertDTO);

                if (request.HasErrors == true)
                    return BadRequest(request.Messages);

                return Ok(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No se ha podido ejecutar la operacion.");
            }

        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult<Result>> Update([FromForm] UserUpdateDTO userUpdateDTO, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Los datos ingresados no son validos");

                var request = await _usersServices.Update(userUpdateDTO, id);

                if (request == null || request.HasErrors == true)
                    return BadRequest(request.Messages);

                return Ok(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No se ha podido ejecutar la operacion.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            try
            {
                var request = await _usersServices.Delete(id);

                if (request.HasErrors == true)
                    return BadRequest(request.Messages);

                return Ok(request);
            }
            catch (Exception e)
            {
                return new Result().Fail(e.Message);
            }
        }
    }
}
