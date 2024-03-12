using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Security;
using Service.Security.Helpers;

namespace Service.Security {
    public class UserService(IUserRepository userRepository) : IUserService {

        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> Create(CreateUserDto oCreateUserDto) {

            // Map CreateUserDto to a user object using some mapper (UserMapper).
            var user = UserMapper.MapUser(oCreateUserDto);

            //Encrypt the user's password using some encryption method (EncryptorHelper).
            user.Password = EncryptorHelper.Encrypt(user.Password);

            try {
                //Attempt to add the new user through the UserRepository and save changes asynchronously.
                await _userRepository.CreateAsync(user);

                await _userRepository.SaveChangesAsync();

                //Return true to indicate successful creation.
                return true;

            } catch (Exception) {
                //If there's an exception during the process, return false.
                return false;
            }
        }
    }
}