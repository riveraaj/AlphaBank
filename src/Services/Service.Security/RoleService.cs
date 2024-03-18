using Data.AlphaBank;
using Dtos.AlphaBank.Security;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Mapper.Security;
using Microsoft.Extensions.Logging;

namespace Service.Security
{
    public class RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger) : IRoleService {

        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ILogger<RoleService> _logger = logger;

        public async Task<bool> Create(CreateRoleDto oCreateRoleDto) {
            // Map CreateRoleDto to a role object using some mapper (RoleMapper)
            var role = RoleMapper.MapRole(oCreateRoleDto);

            try {

                _logger.LogInformation("----- Create Role: Start creating and saving the role in the database");

                // Attempt to add the new role through the RoleRepository and save changes asynchronously.
                await _roleRepository.CreateAsync(role);

                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Role: Successfully completes the process");

                // Return true to indicate successful creation.
                return true;
            } catch (Exception e) {
                _logger.LogError($"----- Create Role: An error occurred while creating and saving to the database. More about error: {e.Message}");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<Role>> GetAll() {
            try {
                //Attempt to retrieve all roles asynchronously from the RoleRepository.
                return (List<Role>) await _roleRepository.GetAllAsync();
            } catch (Exception){
                //If there's an exception during the process, return null.
                return [];
            }
        }
    }
}