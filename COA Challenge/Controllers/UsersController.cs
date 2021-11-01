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
        private readonly IUsersServices<> _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }
        #endregion

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersServices.GetAll();

            var response = new Result<IEnumerable<UserDTO>>();
            response.Success(users);

            return Ok(response);
        }

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

            response.Success(user);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Insert([FromForm] UserInsertDTO userInsertDTO)
        {
            var response = new NCResult();

            if (!ModelState.IsValid)
            {
                await response.Fail("Datos incorrectos");
                return BadRequest(response.Messages);
            }

            var insert = await _usersServices.Insert(userInsertDTO);

            if (insert.HasErrors == true)
                return BadRequest(insert.Messages);

            return Ok(insert.Messages);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDTO userUpdateDTO, int id)
        {
            var response = new COA.Domain.Common.NCResult();

            if (!ModelState.IsValid)
                return BadRequest("Los datos ingresados no son validos");

            var request = await _usersServices.Update(userUpdateDTO, id);

            if (request == null || request.HasErrors == true)
                return BadRequest(request.Messages);

            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            var request = await _usersServices.Delete(id);

            if (request.HasErrors == true)
                return BadRequest(request.Messages);

            return Ok(request);
        }
    }
}
