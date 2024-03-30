namespace Interfaces.GrantingLoans.Services {
    public interface IGrantingLoansService {

        public Task<bool> GrantingLoan(int id);
    }
}