using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface IInterestService {
        public Task<UpdateInterestDTO?> GetById(int id);
        public Task<List<ShowCatalogsDTO>> GetAllForShow();
        public Task<bool> Create(CreateInterestDTO oCreateInterestDTO);
        public Task<List<Interest>> GetAll();
        public Task<bool> Update(UpdateInterestDTO oUpdateInterestDTO);
        public Task<bool> Remove(int id);
    }
}