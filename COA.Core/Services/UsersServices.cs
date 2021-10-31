using AutoMapper;
using COA.Core.Interfaces;
using COA.Domain;
using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using COA.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Core.Services
{
    public class UsersServices : IUsersServices
    {
        #region Fields and Constructor
        private readonly UOW _uow;
        private readonly IMapper _mapper;
        public UsersServices(UOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        #endregion
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            try
            {
                var request = await _uow.UsersRepository.GetAll();

                if (request == null)
                    return null;

                var response = _mapper.Map<IEnumerable<UserDTO>>(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserDTO> GetById(int id)
        {
            try
            {
                if (_uow.UsersRepository.EntityExists(id) == false)
                    return null;

                var request = await _uow.UsersRepository.GetById(id);
                var response = _mapper.Map<User, UserDTO>(request);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Result> Insert(UserInsertDTO userInsertDTO)
        {
            try
            {
                var request = _mapper.Map<UserInsertDTO, User>(userInsertDTO);
                var response= await _uow.UsersRepository.Insert(request);

                return response;
            }
            catch (Exception e)
            {
                return new Result().Fail(e.Message);
            }

        }
    }
}
