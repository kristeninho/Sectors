using SectorsBackend.DTOs;

namespace SectorsBackend.Utils
{
	public class UserValidationHelper
	{
        public static KeyValuePair<bool, string> IsUserValid(UserDTO user)
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

        public static KeyValuePair<bool, string> IsUserNameValid(string userName)
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
