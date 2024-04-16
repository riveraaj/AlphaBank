using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services {
    public interface IRoleService {
        public Task<UpdateRoleDTO?> GetById(int id);
        public Task<List<ShowCatalogsDTO>> GetAllForShow();
        public Task<bool> Create(CreateRoleDTO oCreateRoleDTO);
        public Task<List<Role>> GetAll();
        public Task<bool> Update(UpdateRoleDTO oUpdateRoleDTO);
        public Task<bool> Remove(int id);
    }
}