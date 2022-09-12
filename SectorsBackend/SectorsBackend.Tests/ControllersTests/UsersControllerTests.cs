using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SectorsBackend.Controllers;
using SectorsBackend.DTOs;
using SectorsBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorsBackend.UnitTests.ControllersTests
{
	public class UsersControllerTests
	{
		private readonly Mock<IUsersRepository> usersRepository;
		public UsersControllerTests()
		{
			usersRepository = new Mock<IUsersRepository>();
		}

        [Fact]
        public void GetUserDataTest_WithValidUserName()
        {
            //arrange
            var usersList = GetUserDTOsTestData();

            usersRepository.Setup(x => x.GetUserDataByNameAsync(usersList[0].Name))
				.ReturnsAsync(usersList[0]);

            var usersController = new UsersController(usersRepository.Object);

            //act
            var userResult = usersController.GetUserData(usersList[0].Name).Result.Value;

            //assert
            Assert.NotNull(userResult);
            Assert.Equal(usersList[0], userResult);
        }

		[Theory]
		[InlineData(1, "User has no name")]
        [InlineData(2, "User Name length is not between 3 - 30 characters")] // too short username
        [InlineData(3, "User Name length is not between 3 - 30 characters")] // too long username
        [InlineData(4, "User Name can not contain numbers")]
        public void GetUserDataTest_WithInvalidUserName(int userIndex, string errorMessage)
        {
            //arrange
            var usersList = GetUserDTOsTestData();

            usersRepository.Setup(x => x.GetUserDataByNameAsync(usersList[userIndex].Name))
                .ReturnsAsync(usersList[userIndex]);

            var usersController = new UsersController(usersRepository.Object);

            //act
            var userResult = usersController.GetUserData(usersList[userIndex].Name).Result;

            //assert
            var result = userResult.Result as ObjectResult;
            Assert.Null(userResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(errorMessage, result.Value);
        }

        [Theory]
        [InlineData("User created")]
        [InlineData("User updated")]
        
        public void AddOrUpdateUserTest_WithValidUser(string responseMessage)
        {
            //arrange
            var usersList = GetUserDTOsTestData();

			usersRepository.Setup(x => x.AddOrUpdateUserAsync(usersList[0]))
                .ReturnsAsync(responseMessage);

            var usersController = new UsersController(usersRepository.Object);

            //act
            var userResult = usersController.AddOrUpdateUser(usersList[0]).Result;

            //assert
            var result = userResult.Result as ObjectResult;
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(responseMessage, result.Value);
        }

        [Theory]
        [InlineData(1, "User has no name")]
        [InlineData(2, "User Name length is not between 3 - 30 characters")] // too short name
        [InlineData(3, "User Name length is not between 3 - 30 characters")] // too long name
        [InlineData(4, "User Name can not contain numbers")]
        [InlineData(5, "User is null")]
        [InlineData(6, "User has no sectors")]
        [InlineData(7, "User has not agreed to terms")]
        public void AddOrUpdateUserTest_WithInvalidUser(int userIndex, string errorMessage)
        {
            //arrange
            var usersList = GetUserDTOsTestData();

            usersRepository.Setup(x => x.AddOrUpdateUserAsync(usersList[userIndex]))
                .ReturnsAsync(errorMessage);

            var usersController = new UsersController(usersRepository.Object);

            //act
            var userResult = usersController.AddOrUpdateUser(usersList[userIndex]).Result;

            //assert
            var result = userResult.Result as ObjectResult;            
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(errorMessage, result.Value);
        }

        private static List<UserDTO> GetUserDTOsTestData()
		{
            var userDTOList = new List<UserDTO>()
            {
                new UserDTO()
                {
                     Name = "Kristen Niilop",
                     AgreedToTerms = true,
                     SectorIds = new List<int>{ 4, 5 }
                },
                new UserDTO()
                {
                     Name = null,
                     AgreedToTerms = true,
                     SectorIds = new List<int>{ 23, 53 }
                },
                new UserDTO()
                {
                     Name = "Kr",
                     AgreedToTerms = true,
                     SectorIds = new List<int>{ 23, 53 }
                },
                new UserDTO()
                {
                     Name = "Kriiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiisten",
                     AgreedToTerms = true,
                     SectorIds = new List<int>{ 23, 53 }
                },
                new UserDTO()
                {
                     Name = "Kr1sten",
                     AgreedToTerms = true,
                     SectorIds = new List<int>{ 23, 53 }
                },
                null
                ,
                new UserDTO()
                {
                     Name = "Reelika",
                     AgreedToTerms = true,
                     SectorIds = new List<int>{ 23, 53 }
                },
                new UserDTO()
                {
                     Name = "Reelika",
                     AgreedToTerms = false,
                     SectorIds = new List<int>{ 23, 53 }
                }
            };
            return userDTOList;
		}
    }
}
