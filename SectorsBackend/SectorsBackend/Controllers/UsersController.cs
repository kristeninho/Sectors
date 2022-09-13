using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories.Interfaces;
using SectorsBackend.Utils;

namespace SectorsBackend.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // GET: api/Users/GetUserData
        [HttpGet("GetUserdata")]
        public async Task<ActionResult<UserDTO>> GetUserData(string userName)
        {
            var isUserNameValid = UserValidationHelper.IsUserNameValid(userName);
            if (!isUserNameValid.Key) return BadRequest(isUserNameValid.Value);

            var userData = await _usersRepository.GetUserDataByNameAsync(userName);

            if (userData == null)
            {
                return NotFound("User not found");
            }

            return userData;
        }

		// POST: api/AddOrUpdateUser
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("AddOrUpdateUser")]
        public async Task<ActionResult<User>> AddOrUpdateUser(UserDTO user)
		{
            var isUserValid = UserValidationHelper.IsUserValid(user);

            if (isUserValid.Key)
			{
				var response = await _usersRepository.AddOrUpdateUserAsync(user);

				if (response == "User created" || response == "User updated") return StatusCode(201, response);
				return BadRequest(response);
			}
			return BadRequest(isUserValid.Value);
		}
    }
}
