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

                var response = _mapper.Map<List<UserDTO>>(request);
                
                if (response.Count==0)
                    return null;

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
                var response = await _uow.UsersRepository.Insert(request);

                await _uow.SaveChangesAsync();

                return response;
            }
            catch (Exception e)
            {
                return new Result().Fail(e.Message);
            }

        }
        public async Task<Result> Update(UserUpdateDTO userUpdateDTO, int id)
        {
            try
            {
                if (_uow.UsersRepository.EntityExists(id) == false)
                    return null;

                var userDb = await _uow.UsersRepository.GetById(id);

                if (userUpdateDTO.FirstName != null) { userDb.FirstName = userUpdateDTO.FirstName; };
                if (userUpdateDTO.LastName != null) { userDb.LastName = userUpdateDTO.LastName; };
                if (userUpdateDTO.Phone != null) { if (userUpdateDTO.Phone != userDb.Phone) userDb.Phone = (int)userUpdateDTO.Phone; };

                var response = await _uow.UsersRepository.Update(userDb);

                await _uow.SaveChangesAsync();

                return response;
            }
            catch (Exception e)
            {
                return new Result().Fail(e.Message);
            }

        }
        public async Task<Result> Delete(int id)
        {
            try
            {
                if (_uow.UsersRepository.EntityExists(id) == false)
                    return new Result().Fail("No se ha encontrado un usuario con el Id especificado");

                var response = await _uow.UsersRepository.Delete(id);

                await _uow.SaveChangesAsync();

                return response;

            }
            catch (Exception e)
            {
                return new Result().Fail(e.Message);
            }
        }
    }
}
