﻿using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services
{
    public interface IAccountService
    {

        public Task<bool> Create(CreateAccountDto createAccountDto);

    }
}