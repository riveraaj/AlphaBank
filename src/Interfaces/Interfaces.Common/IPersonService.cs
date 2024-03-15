using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Interfaces.Common {
    public interface IPersonService{

        public Task<Person?> GetById(int id);
        public Task<bool> Create(CreatePersonDto oCreatePersonDto, CreatePhoneDto oCreatePhoneDto);
    }
}