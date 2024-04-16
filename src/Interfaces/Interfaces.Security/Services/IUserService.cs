using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services {
    public interface IUserService {
        public Task<List<ShowUserDTO>> GetAll();
        public Task<UpdateUserDTO?> GetById(int id); 
        public Task<bool> Create(CreateUserDTO oCreateUserDTO);
        public Task<bool> Update(UpdateUserDTO oUpdateUserDTO);
        public Task<bool> Remove(int id);
    }
}