using Interfaces.Security;

namespace Service.Security;
public class UserService(IUserRepository userRepository) : IUserService {

    private readonly IUserRepository _userRepository = userRepository;

    //This method helps us to validate and send a message
    //in response to the user's login process.
    public async Task<string> Login() {

        if (!await ValidateUserId(1))
            return "Invalid identification";

        if (!await ValidateUserPassword(1, "423rwcv"))
            return "Wrong password";

        return  "";
    }

    //This function returns true or false if a user with the credential id is found.
    public async Task<bool> ValidateUserId(int id) { 

        var user = await _userRepository.GetByPersonIdAsync(id);

        if(user == null) return false;
        else return true;
    }

    //This function returns true or false if the contrasenna credentials are correct.
    public async Task<bool> ValidateUserPassword(int id, string password) {

        var user = await _userRepository.GetByPersonIdAsync(id);

        if (user == null) return false;

        return (user.Password.Equals(password));
    }
}