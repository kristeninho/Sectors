using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;

namespace SectorsBackend.Repositories.Interfaces
{
	public interface IUsersRepository
	{
		Task<ActionResult<UserDTO>?> GetUserDataByNameAsync(string userName);
		Task<string> AddOrUpdateUserAsync(UserDTO user);
	}
}