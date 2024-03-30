using Dtos.AlphaBank.Security;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Microsoft.Extensions.Logging;
using Service.Security.Helpers;

namespace Service.Security
{
    public class UserAuthenticatorService(IUserRepository userRepository, 
                                           ILogger<UserAuthenticatorService> logger) 
                                           : IUserAuthenticatorService {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserAuthenticatorService> _logger = logger;

        //This method helps us to validate and send a message
        //in response to the user's login process.
        public async Task<(bool, UserAuthenticationDto?)> UserAuthenticator(UserLoginDto oUserLoginDto) {

            _logger.LogInformation("---- The user's credentials begin to be validated.");

            //We obtain the values returned by the method
            var (validatedUser, userAuth) = await ValidateUserId((int) oUserLoginDto.Id!);

            var validatedUserPassword = await ValidateUserPassword((int) oUserLoginDto.Id,
                                                                    oUserLoginDto.Password);
            //Validate that the outputs are invalid
            if (!validatedUser || !validatedUserPassword) {
                _logger.LogError("---- User authentication failed");
                return (false, null);
            }

            _logger.LogInformation("---- The user was successfully authenticated");

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

            return EncryptorHelper.ValidateEncryption(password, pass);
        }
    }
}