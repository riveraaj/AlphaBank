using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Security;
using Service.Security.Helpers;

namespace Service.Security {
    public class UserService(IUserRepository userRepository) : IUserService {

        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> Create(CreateUserDto oCreateUserDto) {

            var user = UserMapper.MapUser(oCreateUserDto);

            user.Password = EncryptorHelper.Encrypt(user.Password);

            try {

                await _userRepository.CreateAsync(user);

                await _userRepository.SaveChangesAsync();

                return true;

            } catch (Exception) {
                return false;
                throw;
            }
        }
    }
}