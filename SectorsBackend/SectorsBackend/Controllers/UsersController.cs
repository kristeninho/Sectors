using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories;
using SectorsBackend.Repositories.Interfaces;

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
            var isUserNameValid = IsUserNameValid(userName);
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
            var isUserValid = IsUserValid(user);

            if (isUserValid.Key)
			{
				var response = await _usersRepository.AddOrUpdateUserAsync(user);

				if (response == "User created" || response == "User updated") return StatusCode(201, response);
				return BadRequest(response);
			}
			return BadRequest(isUserValid.Value);
		}

		private static KeyValuePair<bool, string> IsUserValid(UserDTO user)
		{
			if (user == null)
            {
                return new KeyValuePair<bool, string>(false, "User is null");
			}
            if (user.SectorIds == null || user.SectorIds.Count == 0)
            {
                return new KeyValuePair<bool, string>(false, "User has no sectors");
            }
            var isUserNameValid = IsUserNameValid(user.Name);
            if (!isUserNameValid.Key)
            {
                return new KeyValuePair<bool, string>(false, isUserNameValid.Value);
            }
            if (!user.AgreedToTerms)
			{
                return new KeyValuePair<bool, string>(false, "User has not agreed to terms");
            }
            return new KeyValuePair<bool, string>(true, "");
        }

        private static KeyValuePair<bool, string> IsUserNameValid(string userName)
        {
            if (userName == null)
            {
                return new KeyValuePair<bool, string>(false, "User has no name");
            }
            if (userName.Length < 3 || userName.Length > 30)
            {
                return new KeyValuePair<bool, string>(false, "User Name length is not between 3 - 30 characters");
            }
            if (userName.Any(char.IsDigit))
            {
                return new KeyValuePair<bool, string>(false, "User Name can not contain numbers");
            }
            return new KeyValuePair<bool, string>(true, "");
        }
    }
}
