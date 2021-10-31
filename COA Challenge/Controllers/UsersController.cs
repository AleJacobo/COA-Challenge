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
                    return NotFound(new Result().Fail($"No se han encontrado registros de usuario"));

                return Ok(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No se ha podido ejecutar la operacion.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetById(int id )
        {
            try
            {
                var request = await _usersServices.GetById(id);

                if (request == null)
                    return NotFound(new Result().Fail($"No se ha encontrado este usuario con el Id especifico"));

                return Ok(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No se ha podido ejecutar la operacion.");
            }
        }
    }
}
