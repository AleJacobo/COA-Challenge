using AutoMapper;
using COA.Core.Interfaces;
using COA.Core.Services;
using COA.Domain;
using COA.Domain.Common;
using COA.Domain.DTOs.UserDTOs;
using COA.Domain.Profiles;
using COA.Infrastructure.Data;
using COA.Infrastructure.Repositories;
using COA.Infrastructure.Repositories.Interfaces;
using COA.Tests.TestHelper;
using COA_Challenge.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace COA.Tests.Tests
{
    public class UsersControllerTests : TestContext, IDisposable
    {
        #region Fields
        private AppDbContext _context;
        private Mock<IUsersServices> _userServicesMock = new();
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
        public async Task GetAllUsers_ShouldReturn_ListofUserDTO_Ok()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act
                var request = await controller.GetAll();
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(200, expected.StatusCode);
                Assert.IsType<List<UserDTO>>(expected.Value);
            }
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturn_BadRequest()
        {
            using (_context)
            {
                //Arrange
                Init();
                var controller = new UsersController(_usersServices);
                SeedEmpty(_context);

                //Act
                var request = await controller.GetAll();
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(400, expected.StatusCode);
            }
        }

        [Fact]
        public async Task GetById_ShouldReturn_User_Ok()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act
                var request = await controller.GetById(1);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(200, expected.StatusCode);
                Assert.IsType<UserDTO>(expected.Value);
            }
        }

        [Fact]
        public async Task GetById_ShouldReturn_BadRequest()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act
                var request = await controller.GetById(12);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(400, expected.StatusCode);
            }
        }

        [Fact]
        public async Task Insert_ShoudReturn_ResultOk()
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
                var request = await controller.Insert(newUser);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(200, expected.StatusCode);
                Assert.IsType<Result>(expected.Value);
            }
        }

        [Fact]
        public async Task Insert_BadParameter_ShoudReturn_BadResult()
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
                var request = await controller.Insert(newUser);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(400, expected.StatusCode);
                Assert.IsType<Result>(expected.Value);
            }
        }

        [Fact]
        public async Task Update_ShouldReturn_ResultOk()
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
                var request = await controller.Update(updateUser,1);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(200, expected.StatusCode);
                Assert.IsType<Result>(expected.Value);
            }
        }

        [Fact]
        public async Task Update_BadParameter_ShouldReturn_ResultBadRequest()
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
                var request = await controller.Update(updateUser, 1);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(400, expected.StatusCode);
            }
        }

        [Fact]
        public async Task Delete_ShouldReturn_ResultOk()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);

                var controller = new UsersController(_usersServices);

                //Act
                var request = await controller.Delete(1);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(200, expected.StatusCode);
                Assert.IsType<Result>(expected.Value);
            }
        }

        [Fact]
        public async Task Delete_EntityDoesntExists_ShouldReturn_ResultBadRequest()
        {
            using (_context)
            {
                //Arrange
                Init();
                SeedUsers(_context);
                var controller = new UsersController(_usersServices);

                //Act
                var request = await controller.Delete(15);
                var expected = (ObjectResult)request.Result;

                //Assert
                Assert.Equal(400, expected.StatusCode);
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
