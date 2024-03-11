using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Service.Security.Helpers;

namespace Service.Security
{
    public class UserService(IUserRepository userRepository) : IUserService {

        private readonly IUserRepository _userRepository = userRepository;

        //This method helps us to validate and send a message
        //in response to the user's login process.
        public async Task<(bool, UserAuthenticationDto?)> UserAuthenticator(UserLoginDto oUserLoginDto) {

            //We obtain the values returned by the method
            var (validatedUser, userAuth) = await ValidateUserId((int) oUserLoginDto.Id!);

            var validatedUserPassword = await ValidateUserPassword((int) oUserLoginDto.Id,
                                                                    oUserLoginDto.Password);
            //Validate that the outputs are invalid
            if (!validatedUser && !validatedUserPassword)
                return (false, null);

            return (true, userAuth);
        }

        //This function returns true or false if a user with the credential id is found.
        public async Task<(bool, UserAuthenticationDto?)> ValidateUserId(int id) {

            var user = await _userRepository.GetByPersonIdAsync(id);

            if (user == null) return (false, null);
            else return (true, new UserAuthenticationDto
            { Id = user.Id.ToString(), Role = user.RoleId.ToString() });
        }

        //This function returns true or false if the password credentials are correct.
        public async Task<bool> ValidateUserPassword(int id, string password) {

            var user = await _userRepository.GetByPersonIdAsync(id);

            if (user == null) return false;

            var pass = user.Password;

            return (pass.Equals(EncryptorHelper.ValidateEncryption(password, pass)));
        }
    }
}