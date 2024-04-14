using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class RoleMapper {

        public static Role MapRole(CreateRoleDTO oCreateRoleDTO)
            => new() {
                Description = oCreateRoleDTO.Description
            };

        public static Role MapUpdateRole(UpdateRoleDTO oUpdateRoleDTO)
            => new()
            {
                Id = oUpdateRoleDTO.Id,
                Description = oUpdateRoleDTO.Description
            };
    }
}