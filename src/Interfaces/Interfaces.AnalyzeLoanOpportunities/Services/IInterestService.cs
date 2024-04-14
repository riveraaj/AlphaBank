using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface IInterestService {
        public Task<bool> Create(CreateInterestDTO oCreateInterestDTO);
        public Task<List<Interest>> GetAll();
        public Task<bool> Update(UpdateInterestDTO oUpdateInterestDTO);
        public Task<bool> Remove(int id);
    }
}