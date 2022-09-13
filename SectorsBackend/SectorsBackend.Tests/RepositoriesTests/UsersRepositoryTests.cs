using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories;

namespace SectorsBackend.UnitTests.RepositoriesTests
{
	public class UsersRepositoryTests
	{
		private readonly IDbContextFactory<AppDbContext> _context;
		private readonly UsersRepository _repository;
		public UsersRepositoryTests()
		{
			_context = new TestDbContextFactory();
			_repository = new UsersRepository(_context);
			InitializeUsersDatabase(_context);
		}

		private void InitializeUsersDatabase(IDbContextFactory<AppDbContext> context)
		{
			using var dbContext = _context.CreateDbContext();

			var users = new List<User>();
			foreach (var u in GetUsersTestData())
			{
				var userSectors = u.Sectors.Select(x => x.Id).ToList();
				var user = new User
				{
					AgreedToTerms = u.AgreedToTerms,
					Name = u.Name,
					Sectors = dbContext.Sectors.Where(s => userSectors.Contains(s.Id)).ToArray()
				};
				users.Add(user);
			}

			dbContext.Users.AddRange(users);
			dbContext.SaveChanges();
		}

		[Fact]
		public void GetUserDataByNameAsyncTest_UserExists()
		{
			//arrange
			var usersTestData = GetUsersTestData();

			//act
			var userDTOExpected = GetUserDTOsTestData()[0];
			var userDTOActual = _repository.GetUserDataByNameAsync(usersTestData[0].Name).Result.Value;
			var expectedJson = System.Text.Json.JsonSerializer.Serialize(userDTOExpected);
			var actualJson = System.Text.Json.JsonSerializer.Serialize(userDTOActual);

			//assert
			Assert.NotNull(userDTOActual);
			Assert.Equal(userDTOExpected.SectorIds, userDTOActual.SectorIds);
			Assert.Equal(expectedJson, actualJson);
		}

		[Fact]
		public void GetUserDataByNameAsyncTest_UserDoesNotExist()
		{
			//arrange

			//act
			var userDTOActual = _repository.GetUserDataByNameAsync("Random False Name That Does Not Exist").Result;

			//assert
			Assert.Null(userDTOActual);
		}

		[Theory]
		[InlineData("User created", 1)]
		[InlineData("User updated", 2)]
		[InlineData("Error - Some of the sectors do not exist", 3)]
		[InlineData("Error - Some of the sectors can not be added", 4)]
		public void AddOrUpdateUserAsyncTest_AddUser(string responseMessage, int userDTOIndex)
		{
			//arrange
			var usersDTOsTestData = GetUserDTOsTestData();

			//act
			var expectedResult = responseMessage;
			var actualResult = _repository.AddOrUpdateUserAsync(usersDTOsTestData[userDTOIndex]).Result;

			//assert
			Assert.Equal(expectedResult, actualResult);
		}

		private static List<User> GetUsersTestData()
		{
			var usersList = new List<User>()
			{
				new User()
				{
					 Id = 1,
					 Name = "Kristen Niilop",
					 AgreedToTerms = true,
					 Sectors = new List<Sector>
					 {
						 new Sector
							{
								Id = 3,
								Name = "Construction materials",
								HasSubSectors = false,
								ParentId = 1
							},
							new Sector
							{
								Id = 4,
								Name = "Electronics and Optics",
								HasSubSectors = false,
								ParentId = 1
							} 
						
					 }
				},
			};
			return usersList;
		}
		private static List<UserDTO> GetUserDTOsTestData()
		{
			var userDTOList = new List<UserDTO>()
			{
				new UserDTO()
				{
					AgreedToTerms = true,
					Name = "Kristen Niilop",
					SectorIds = new List<int>{3, 4}
				},
				new UserDTO()
				{
					AgreedToTerms = true,
					Name = "Taavi Merisiga",
					SectorIds = new List<int>{3}
				},
				new UserDTO()
				{
					AgreedToTerms = true,
					Name = "Taavi Merisiga",
					SectorIds = new List<int>{3, 4}
				},
				new UserDTO()
				{
					AgreedToTerms = true,
					Name = "Rando Raab",
					SectorIds = new List<int>{3, 99}
				},
				new UserDTO()
				{
					AgreedToTerms = true,
					Name = "Rando Raab",
					SectorIds = new List<int>{1}
				}
			};
			return userDTOList;
		}
	}
}
