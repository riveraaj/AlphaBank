using Data.AlphaBank;
using Dtos.AlphaBank.Security;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Mapper.Security;
using Microsoft.Extensions.Logging;
using Service.Security.Helpers;

namespace Service.Security {
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<bool> UserSetup(CreateEmployeeDTO oCreateEmployeeDTO, IEmployeeRepository oEmployeeRepository) {

            //The id of the last registered employee is obtained to assign the user to the employee
            var employee = await oEmployeeRepository.GetLastEmployeeAsync();
            var employeeId = employee!.Id;

            //The searched id is assigned to the model to perform the registration.
            oCreateEmployeeDTO.User.EmployeeId = employeeId;

            //The user service is called to create the user.
            var result = await Create(oCreateEmployeeDTO.User);

            if (!result) {
                //If there's an exception during the process return false.
                return false;
            }
            // Return true to indicate successful creation.
            return true;
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