using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Security;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Mapper.Security;
using Microsoft.Extensions.Logging;

namespace Service.Security {
    public class RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger) : IRoleService {

        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ILogger<RoleService> _logger = logger;

        public async Task<UpdateRoleDTO?> GetById(int id) {
            try {
                var typeCurrency = await _roleRepository.GetById(id);
                return RoleMapper.MapUpdateRole(typeCurrency!);
            } catch {
                return null;
            }
        }

        public async Task<List<ShowCatalogsDTO>> GetAllForShow() {
            try {
                //Attempt to retrieve all deadline asynchronously from the DeadlineRepository.
                var deadlineList = await _roleRepository.GetAllAsync();
                var showDeadlineList = new List<ShowCatalogsDTO>();
                foreach (var deadline in deadlineList)
                    showDeadlineList.Add(RoleMapper.MapShowRoleDTO(deadline));

                return showDeadlineList;
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Create(CreateRoleDTO oCreateRoleDTO)
        {
            // Map CreateRoleDto to a role object using some mapper (RoleMapper)
            var role = RoleMapper.MapRole(oCreateRoleDTO);

            try
            {

                _logger.LogInformation("----- Create Role: Start creating and saving the role in the database");

                // Attempt to add the new role through the RoleRepository and save changes asynchronously.
                await _roleRepository.CreateAsync(role);

                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Role: Successfully completes the process");

                // Return true to indicate successful creation.
                return true;
            }
            catch
            {
                _logger.LogError("----- Create Role: An error occurred while creating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<Role>> GetAll()
        {
            try
            {
                //Attempt to retrieve all roles asynchronously from the RoleRepository.
                return (List<Role>)await _roleRepository.GetAllAsync();
            }
            catch (Exception)
            {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Update(UpdateRoleDTO oUpdateRoleDTO)
        {
            // Map UpdateRoleDto to a role object using some mapper (RoleMapper)
            var role = RoleMapper.MapUpdateRole(oUpdateRoleDTO);

            try
            {

                _logger.LogInformation("----- Update Role: Start updating and saving the role in the database");

                // Attempt to update a role through the RoleRepository and save changes asynchronously.
                await _roleRepository.UpdateAsync(role);

                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Role: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Update Role: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                _logger.LogInformation("----- Remove Role: Start removing a role and saving the changes in the database");

                // Attempt to remove a role through the RoleRepository and save changes asynchronously.
                await _roleRepository.RemoveAsync(id);

                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove Role: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Remove Role: An error occurred while removing a role and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }
    }
}