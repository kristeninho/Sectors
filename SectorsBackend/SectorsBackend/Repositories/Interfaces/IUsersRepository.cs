using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;

namespace SectorsBackend.Repositories.Interfaces
{
	public interface IUsersRepository
	{
		public Task<ActionResult<UserDTO>?> GetUserDataByNameAsync(string userName);
		public Task<string> AddOrUpdateUserAsync(UserDTO user);
	}
}