using AutoMapper;
using COA.Core.Interfaces;
using COA.Core.Services;
using COA.Domain;
using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using COA.Domain.Exceptions;
using COA.Infrastructure.Data;
using COA.Infrastructure.Repositories;
using COA.Infrastructure.Repositories.Interfaces;
using COA.Tests.TestHelper;
using COA_Challenge.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace COA.Tests.Tests
{
    public class UsersControllerTests : TestContext, IDisposable
    {
        #region Fields
        private AppDbContext _context;
        private IUsersServices _usersServices;
        private IUnitOfWork _uow;
        private IMapper _mapper;
        #endregion

        [Fact]
        public void Init()
        {
            _context = GetTestContext(Guid.NewGuid().ToString());
            _uow = new UOW(_context);
            _mapper = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();
            _usersServices = new UsersServices(_uow, _mapper);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturn_Result_ListOfUsers_Ok()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act
                var users = await controller.GetAll();
                var result = users as OkObjectResult;

                //Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
                Assert.IsType<Result<IEnumerable<UserDTO>>>(result.Value);
            }
        }

        [Fact]
        public async Task GetAllUsers_ShouldThrowException()
        {
            using (_context)
            {
                //Arrange
                Init();
                var controller = new UsersController(_usersServices);

                //Act and Assert
                await Assert.ThrowsAsync<COAException>(async () => await controller.GetAll());
            }
        }

        [Fact]
        public async Task GetById_ShouldReturn_Result_User_Ok()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act
                var user = await controller.GetById(1);
                var result = user as OkObjectResult;

                //Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
                Assert.IsType<UserDTO>(result.Value);
            }
        }

        [Fact]
        public async Task GetById_EntityDoesntExixts_ShouldThrowException()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act & Assert
                await Assert.ThrowsAsync<COAException>(async () => await controller.GetById(9999999));
            }
        }

        [Fact]
        public async Task Insert_ShoudReturn_Ok()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                var newUser = new UserInsertDTO()
                {
                    FirstName = "TestName",
                    LastName = "TestLastName",
                    Email = "test@example.com",
                    Phone = 111030852
                };

                //Act
                var insert = await controller.Insert(newUser);
                var result = insert as OkResult;

                //Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
        }

        [Fact]
        public async Task Insert_BadParameter_ShouldThrowBadRequest()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                var newUser = new UserInsertDTO()
                {
                    FirstName = "TestName",
                    LastName = "TestLastName",
                    Email = "testexample.com", //<= BadParameter
                    Phone = 111030852
                };
                controller.ModelState.AddModelError("Email", "The Email field is not a valid e-mail address.");

                //Act

                var insert = await controller.Insert(newUser);
                var result = insert as BadRequestObjectResult;

                //Assert
                Assert.NotNull(result);
                Assert.Equal(400, result.StatusCode);
            }
        }

        [Fact]
        public async Task Update_ShouldReturn_Ok()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);

                var controller = new UsersController(_usersServices);

                var updateUser = new UserUpdateDTO()
                {
                    FirstName = "UpdateName",
                    LastName = "UpdateLastName",
                    Email = "testUpdate@example.com",
                    Phone = 99999999
                };

                //Act
                var update = await controller.Update(updateUser, 1);
                var result = update as OkResult;

                //Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
        }

        [Fact]
        public async Task Update_BadParameter_ShouldReturn_BadRequest()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);

                var controller = new UsersController(_usersServices);

                var updateUser = new UserUpdateDTO()
                {
                    FirstName = "UpdateName",
                    LastName = "UpdateLastName",
                    Email = "testUpdateexample.com", //<= Bad Parameter
                    Phone = 99999999
                };
                controller.ModelState.AddModelError("Email", "The Email field is not a valid e-mail address.");

                //Act
                var update = await controller.Update(updateUser,1);
                var result = update as BadRequestObjectResult;

                //Assert
                Assert.NotNull(result);
                Assert.Equal(400, result.StatusCode);
            }
        }

        [Fact]
        public async Task Delete_ShouldReturn_Ok()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);

                var controller = new UsersController(_usersServices);

                //Act
                var delete = await controller.Delete(1);
                var result = delete as OkResult;

                //Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
        }

        [Fact]
        public async Task Delete_EntityDoesntExists_ShouldThrowException()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act and Assert
                await Assert.ThrowsAsync<COAException>(async () => await controller.Delete(9999999));
            }
        }

        #region Seed and Dispose
        public void Dispose()
        {
            _context.Dispose();
        }
        private void SeedUsers(AppDbContext _context)
        {
            for (int i = 0; i < 11; i++)
            {
                var user = new User
                {
                    FirstName = $"Seed First Name {i}",
                    LastName = $"Seed Last Name {i}",
                    Email = $"seed{i}@test.com",
                    Phone = 113831379 + i
                };
                _context.Add(user);
            }
            _context.SaveChangesAsync();
        }
        private void SeedEmpty(AppDbContext _context)
        {
            _context.SaveChangesAsync();
        }
        #endregion
    }
}
