﻿using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public class LoanStatementRepository(AlphaBankDbContext context) : ILoanStatementRepository
    {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<LoanStatement>> GetAllAsync()
            => await _context.LoanStatements.ToListAsync();

    }
}
