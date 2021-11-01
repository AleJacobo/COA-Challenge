using AutoMapper;
using COA.Core.Interfaces;
using COA.Domain;
using COA.Domain.DTOs.UserDTOs;
using COA.Domain.Exceptions;
using COA.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Core.Services
{
    public class UsersServices : IUsersServices
    {
        #region Fields and Constructor
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UsersServices(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        #endregion
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var request = await _uow.UsersRepository.GetAll();

            var response = _mapper.Map<List<UserDTO>>(request);

            return response;
        }

        public async Task<UserDTO> GetById(int id)
        {
            if (_uow.UsersRepository.EntityExists(id) == false)
                throw new COAException("No se ha encontrado el usuario");

            var request = await _uow.UsersRepository.GetById(id);
            var response = _mapper.Map<User, UserDTO>(request);

            return response;
        }

        public async Task Insert(UserInsertDTO userInsertDTO)
        {
            var request = _mapper.Map<UserInsertDTO, User>(userInsertDTO);
            await _uow.UsersRepository.Insert(request);
            await _uow.SaveChangesAsync();
        }

        public async Task Update(UserUpdateDTO userUpdateDTO, int id)
        {
            if (_uow.UsersRepository.EntityExists(id) == false)
                throw new COAException("No se ha encontrado el usuario a modificar");

            var userDb = await _uow.UsersRepository.GetById(id);

            if (userUpdateDTO.FirstName != null) { userDb.FirstName = userUpdateDTO.FirstName; };
            if (userUpdateDTO.LastName != null) { userDb.LastName = userUpdateDTO.LastName; };
            if (userUpdateDTO.Phone != null) { if (userUpdateDTO.Phone != userDb.Phone) userDb.Phone = (int)userUpdateDTO.Phone; };

            await _uow.UsersRepository.Update(userDb);
            await _uow.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (_uow.UsersRepository.EntityExists(id) == false)
                throw new COAException("No se ha encontrado el usuario a eliminar");

            await _uow.UsersRepository.Delete(id);
            await _uow.SaveChangesAsync();
        }

    }
}