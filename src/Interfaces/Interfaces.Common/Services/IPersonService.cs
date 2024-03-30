using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Interfaces.Common.Services {
    public interface IPersonService {

        public Task<Person?> GetById(int id);
        public Task<bool> Create(CreatePersonDTO oCreatePersonDTO, CreatePhoneDTO oCreatePhoneDTO);
    }
}