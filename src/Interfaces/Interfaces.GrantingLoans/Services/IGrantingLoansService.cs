﻿namespace Interfaces.GrantingLoans.Services {
    public interface IGrantingLoansService {
        public Task<bool> GrantingLoan(int idLoanApplication, bool grantingLoan);
    }
}