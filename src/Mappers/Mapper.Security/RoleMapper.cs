using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class RoleMapper {

        public static ShowCatalogsDTO MapShowRoleDTO(Role oRole)
           => new() {
               Id = oRole.Id.ToString(),
               Description = oRole.Description
           };

        public static Role MapRole(CreateRoleDTO oCreateRoleDTO)
            => new() {
                Description = oCreateRoleDTO.Description
            };

        public static Role MapUpdateRole(UpdateRoleDTO oUpdateRoleDTO)
            => new() {
                Id = oUpdateRoleDTO.Id,
                Description = oUpdateRoleDTO.Description
            };

        public static UpdateRoleDTO MapUpdateRole(Role oRole)
            => new() {
                Id = oRole.Id,
                Description = oRole.Description
            };
    }
}