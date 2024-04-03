﻿using Data.AlphaBank;

namespace Interfaces.Common.Services {
    public interface ILoanService {
        public Task<bool> Create(LoanApplication oLoanApplication);
        public Task UpdateStatement(int id, byte loanStatementId);
        public Task UpdateQuotas(int id);
    }
}