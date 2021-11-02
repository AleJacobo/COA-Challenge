using COA.Core.Interfaces;
using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersServices.GetAll();

            var response = new Result<IEnumerable<UserDTO>>();
            await response.Success(users);

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

            await response.Success(user);
            return Ok(user);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersServices.Delete(id);
            return Ok();
        }
    }
}
