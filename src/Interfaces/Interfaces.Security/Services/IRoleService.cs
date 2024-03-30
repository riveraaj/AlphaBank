using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services {
    public interface IRoleService {
        public Task<bool> Create(CreateRoleDTO oCreateRoleDTO);
        public Task<List<Role>> GetAll();
    }
}