using Dtos.AlphaBank.Security;

namespace Interfaces.Security
{
    public interface IUserAuthenticatorService {
        public Task<(bool, UserAuthenticationDto?)> UserAuthenticator(UserLoginDto oUserLoginDto);
        public Task<(bool, UserAuthenticationDto?)> ValidateUserId(int id);
        public Task<bool> ValidateUserPassword(int id, string password);
    }
}