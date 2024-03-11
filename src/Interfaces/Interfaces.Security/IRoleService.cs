using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Interfaces.Security {
    public interface IRoleService {

        public Task<bool> Create(CreateRoleDto oCreateRoleDto);

        public Task<List<Role>> GetAll();
    }
}