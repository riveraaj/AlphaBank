using Dtos.AlphaBank.Security;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Mapper.Security;
using Microsoft.Extensions.Logging;
using Service.Security.Helpers;

namespace Service.Security {
    public class UserService(IUserRepository userRepository,
                             ILogger<UserService> logger) : IUserService {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<UpdateUserDTO?> GetById(int id) {
            try {
                //Get user by id.
                var user = await _userRepository.GetByIdAsync(id);
                user.Password = EncryptorHelper.Encrypt(user.Password);
                return UserMapper.MapUpdateUser(user);
            } catch {
                // If there's an exception during the process, return null.
                return null;
            }
        }

        public async Task<List<ShowUserDTO>> GetAll(){
            try {
                //Retrieve Contracts by LoanApplicationId asynchronously from the ContractRepository.
                var userList = await _userRepository.GetAllAsync();

                var showUserDTOList = new List<ShowUserDTO>();

                foreach (var user in userList)
                    showUserDTOList.Add(UserMapper.MapShowUserDTO(user));

                return showUserDTOList;
            }
            catch {
                return [];
            }
        }

        public async Task<bool> Create(CreateUserDTO oCreateUserDTO) {

            // Map CreateUserDto to a user object using some mapper (UserMapper).
            var user = UserMapper.MapUser(oCreateUserDTO);

            //Encrypt the user's password using some encryption method (EncryptorHelper).
            user.Password = EncryptorHelper.Encrypt(user.Password);

            try {
                _logger.LogInformation("----- Create User: Start the creation of an user registry");

                //Attempt to add the new user through the UserRepository and save changes asynchronously.
                await _userRepository.CreateAsync(user);
                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create User: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            } catch {
                _logger.LogError("----- Create User: An error occurred while creating and saving to the database.");

                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<bool> Update(UpdateUserDTO oUpdateUserDTO)
        {
            // Map UpdateUserDTO to a user object using some mapper (UserMapper)
            var user = UserMapper.MapUpdateUser(oUpdateUserDTO);

            //Encrypt the user's password using some encryption method (EncryptorHelper).
            user.Password = EncryptorHelper.Encrypt(user.Password);

            try
            {

                _logger.LogInformation("----- Update User: Start updating and saving the user in the database");

                // Attempt to update a user through the UserRepository and save changes asynchronously.
                await _userRepository.UpdateAsync(user);

                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update User: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Update User: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                _logger.LogInformation("----- Remove User: Start removing a user and saving the changes in the database");

                // Attempt to remove a user through the UserRepository and save changes asynchronously.
                await _userRepository.RemoveAsync(id);

                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove User: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch 
            { 
                _logger.LogError("----- Remove User: An error occurred while removing a user and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }
    }
}