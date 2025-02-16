﻿using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface ILoanApplicationRepository {
        public Task<ICollection<LoanApplication>> GetAllAsync();
        public Task<LoanApplication?> GetById(int id);
        public Task<LoanApplication?> GetByIdForContract(int id);
        public Task Create(LoanApplication oLoanApplication);
        public Task UpdateApplicationStatus(int id, byte statusId);
        public Task SaveChangesAsync();
    }
}