using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories;

namespace SectorsBackend.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;
        public UsersController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // GET: api/Users/GetUserData
        [HttpGet("GetUserdata")]
        public async Task<ActionResult<UserDTO>> GetUserData(string userName)
        {
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
				return Problem(response);
			}
			return Problem(isUserValid.Value);
		}

		private static KeyValuePair<bool, string> IsUserValid(UserDTO user)
		{
			if (user == null)
            {
                return new KeyValuePair<bool, string>(false, "User is null");
			}
            if (user.SectorIds == null)
            {
                return new KeyValuePair<bool, string>(false, "User has no sectors");
            }
            if (user.Name == null)
			{
                return new KeyValuePair<bool, string>(false, "User has no name");
            }
            if (user.Name.Length < 3 || user.Name.Length > 30)
			{
                return new KeyValuePair<bool, string>(false, "User Name length is not between 3 - 30 characters");
            }
			if (!user.AgreedToTerms)
			{
                return new KeyValuePair<bool, string>(false, "User has not agreed to terms");
            }
            return new KeyValuePair<bool, string>(true, "");
        }
	}
}
