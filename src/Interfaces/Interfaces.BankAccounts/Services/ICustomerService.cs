﻿using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services {
    public interface ICustomerService {
        public Task<bool> Create(CreateCustomerDTO oCreateCustomerDTO);

        public Task<List<ShowCustomerDTO>> GetAll();

        public Task<ShowCustomerLoanDTO?> GetByIdForLoan(int id);

        public Task<ShowCustomerDTO?> GetByIdForAccount(int id);
    }
}