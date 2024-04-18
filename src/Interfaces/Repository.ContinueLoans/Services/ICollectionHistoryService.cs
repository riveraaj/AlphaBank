using Data.AlphaBank;
using Dtos.AlphaBank.ContinueLoans;

namespace Interfaces.ContinueLoans.Services {
    public interface ICollectionHistoryService {
        public Task<bool> Create(CreateCollectionHistoryDTO oCreateCollectionHistoryDTO);
        public Task<List<CollectionHistory>> GetAll();
        public  Task<List<ShowLoanDefaultersDTO>> GetAllForDefaultersReport();
    }
}