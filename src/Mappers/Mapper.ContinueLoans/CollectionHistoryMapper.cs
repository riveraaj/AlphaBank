using Data.AlphaBank;
using Dtos.AlphaBank.ContinueLoans;

namespace Mapper.ContinueLoans {
    public static class CollectionHistoryMapper {

        public static CollectionHistory MapCollectionHistory(CreateCollectionHistoryDTO oCreateCollectionHistoryDTO)
            => new() {
                DepositMount = (int) oCreateCollectionHistoryDTO.DepositAmount!,
                LoanId = (int) oCreateCollectionHistoryDTO.LoanId!,
            };
    }
}