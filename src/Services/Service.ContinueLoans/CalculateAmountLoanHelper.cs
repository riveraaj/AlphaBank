using Interfaces.Common.Repositories;
using Interfaces.ContinueLoans.Repositories;

namespace Service.ContinueLoans {
    public class CalculateAmountLoanHelper(ILoanRepository loanRepository,
                                           ICollectionHistoryRepository collectionHistoryRepository) {

        private readonly ILoanRepository _loanRepository = loanRepository;
        private readonly ICollectionHistoryRepository _collectionHistoryRepository
            = collectionHistoryRepository;

        public async Task<decimal> CalculateLoanAmount(int loanId) {
            try  {
                decimal moratoriumInterest = 0;
                decimal totalLoanAmount = 0;

                var collectionList = await _collectionHistoryRepository.GetByLoanId(loanId);
                var loan = await _loanRepository.GetById(loanId);

                foreach (var item in collectionList)
                    moratoriumInterest += item.MoratoriumInterest;

                // Calculate the amount of each installment
                var quotaAmount = loan!.LoanApplication.Amount / int.Parse(loan!.LoanApplication.Deadline.Description.Split(' ')[0]);

                //Calculate the remaining debt by multiplying the amount of each installment by the number of remaining installments.
                var remainingLoan = quotaAmount * loan!.RemainingQuotas;

                totalLoanAmount = remainingLoan + moratoriumInterest;

                return totalLoanAmount;
            } catch {
                return 0;
            }
        }
    }
}