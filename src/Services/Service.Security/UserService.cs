using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Security;
using Microsoft.Extensions.Logging;
using Service.Security.Helpers;

namespace Service.Security {
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<bool> Create(CreateUserDto oCreateUserDto) {

            // Map CreateUserDto to a user object using some mapper (UserMapper).
            var user = UserMapper.MapUser(oCreateUserDto);

            //Encrypt the user's password using some encryption method (EncryptorHelper).
            user.Password = EncryptorHelper.Encrypt(user.Password);

            try {
                _logger.LogInformation("--- Start the creation of a user and save in database");

                //Attempt to add the new user through the UserRepository and save changes asynchronously.
                await _userRepository.CreateAsync(user);

                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("--- I finish saving successfully");

                //Return true to indicate successful creation.
                return true;

            } catch (Exception e) {
                //If there's an exception during the process, return false.
                _logger.LogInformation($"--- An error occurred while creating and saving to the database. More about error: {e.Message}");

                return false;
            }
        }
    }
}