﻿using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface IDeadlineService {
        public Task<UpdateDeadlineDTO?> GetById(int id);
        public Task<List<ShowCatalogsDTO>> GetAllForShow();
        public Task<bool> Create(CreateDeadlineDTO oCreateDeadlineDTO);
        public Task<List<Deadline>> GetAll();
        public Task<bool> Update(UpdateDeadlineDTO oUpdateDeadlineDTO);
        public Task<bool> Remove(int id);
    }
}