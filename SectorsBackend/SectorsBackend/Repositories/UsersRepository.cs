using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories.Interfaces;

namespace SectorsBackend.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		private readonly IDbContextFactory<AppDbContext> _context;

		public UsersRepository(IDbContextFactory<AppDbContext> context)
		{
			_context = context;
		}

		public async Task<ActionResult<UserDTO>?> GetUserDataByNameAsync(string userName)
		{
			using var context = _context.CreateDbContext();

			if (await UserExists(userName, context))
			{
				User user = await GetUserDataWithSectorsAsync(userName, context);

				var userDTO = new UserDTO
				{
					Name = userName,
					AgreedToTerms = user.AgreedToTerms,
					SectorIds = user.Sectors.Select(s => s.Id).ToList()
				};

				return userDTO;
			}
			return null;
		}

		public async Task<string> AddOrUpdateUserAsync(UserDTO user)
		{
			using var context = _context.CreateDbContext();

			if (context.Users == null) return "Error - No users database";

			var areUserSectorsValidPair = AreUserSectorsValid(context, user);
			if (!areUserSectorsValidPair.Key) return "Error - " + areUserSectorsValidPair.Value;

			if (await UserExists(user.Name, context))
			{
				//user with such name exists already - update the user
				return await UpdateUserAsync(user, context);
			}

			//user with such name DOES NOT exist - create the user
			return await CreateUserAsync(user, context);
		}

		private static async Task<string> CreateUserAsync(UserDTO user, AppDbContext context)
		{
			var u = new User
			{
				AgreedToTerms = user.AgreedToTerms,
				Name = user.Name,
				Sectors = await SetUserSectorsAsync(user, context)
			};

			context.Users.Add(u);
			await context.SaveChangesAsync();

			return "User created";
		}

		private static async Task<string> UpdateUserAsync(UserDTO user, AppDbContext context)
		{
			User uptadedUser = await GetUserDataWithSectorsAsync(user.Name, context);
			uptadedUser.Sectors = await SetUserSectorsAsync(user, context);

			await context.SaveChangesAsync();

			return "User updated";
		}

		private static async Task<List<Sector>> SetUserSectorsAsync(UserDTO user, AppDbContext context)
		{
			return await context.Sectors.Where(s => user.SectorIds.Contains(s.Id)).AsQueryable().ToListAsync();
		}

		private static async Task<User> GetUserDataWithSectorsAsync(string userName, AppDbContext context)
		{
			return await context.Users.Include("Sectors").FirstAsync(u => u.Name.ToUpper() == userName.ToUpper());
		}

		private static KeyValuePair<bool, string> AreUserSectorsValid(AppDbContext context, UserDTO user)
		{
			var sectorsList = context.Sectors.ToList();

			// User can not add sectors, if any of them don't exist in the database already
			foreach(var sectorId in user.SectorIds)
			{
				if (!sectorsList.Any(x => x.Id == sectorId)) return new KeyValuePair<bool, string>(false, "Some of the sectors do not exist");
			}

			// User can not have sectors in database, that have subsectors
			if (sectorsList.Where(x => x.HasSubSectors == true).Any(s => user.SectorIds.Contains(s.Id))) return new KeyValuePair<bool, string>(false, "Some of the sectors can not be added");

			return new KeyValuePair<bool, string>(true, "");
		}

		private static async Task<bool> UserExists(string userName, AppDbContext context)
		{
			return await context.Users.AnyAsync(u => u.Name.ToUpper() == userName.ToUpper());
		}
	}
}
