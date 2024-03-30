using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services {
    public interface IUserAuthenticatorService {

        public Task<(bool, UserAuthenticationDTO?)> UserAuthenticator(UserLoginDTO oUserLoginDTO);

        public Task<(bool, UserAuthenticationDTO?)> ValidateUserId(int id);

        public Task<bool> ValidateUserPassword(int id, string password);
    }
}