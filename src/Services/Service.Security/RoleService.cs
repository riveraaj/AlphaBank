using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Security;

namespace Service.Security {
    public class RoleService(IRoleRepository roleRepository) {

        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<bool> Create(CreateRoleDto oCreateRoleDto) {

            var role = RoleMapper.MapRole(oCreateRoleDto);

            try {

                await _roleRepository.CreateAsync(role);
                await _roleRepository.SaveChangesAsync();

                return true;

            } catch (Exception) {
                return false;
                throw;
            }
        }
    }
}