﻿using COA.Core.Interfaces;
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
                    return NoContent();

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
                    return NoContent();

                return Ok(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No se ha podido ejecutar la operacion.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Result>> Insert([FromForm]UserInsertDTO userInsertDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status406NotAcceptable,
                        "Los parametros ingresados no son correctos, intente de nuevo");

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
    }
}
