﻿using Dtos.AlphaBank.ContinueLoans;
using Interfaces.ContinueLoans.Repositories;
using Mapper.ContinueLoans;
using Microsoft.Extensions.Logging;

namespace Service.ContinueLoans {
    public class CollectionHistoryService(ICollectionHistoryRepository collectionHistoryRepository,
                                          ILogger<CollectionHistoryService> logger) {

        private readonly ICollectionHistoryRepository _collectionHistoryRepository
            = collectionHistoryRepository;

        private readonly ILogger<CollectionHistoryService> _logger = logger;

        public async Task<bool> Create(CreateCollectionHistoryDTO oCreateCollectionHistoryDTO) {
            try {
                // Create collection history
                var collectionHistory = CollectionHistoryMapper.MapCollectionHistory(oCreateCollectionHistoryDTO);
                collectionHistory.DateDeposit = DateOnly.FromDateTime(DateTime.Now);
                

                _logger.LogInformation("----- Create  Collection History: Start the creation of a collection history registry");

                await _collectionHistoryRepository.CreateAsync(collectionHistory);
                await _collectionHistoryRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create  Collection History: Creation completed and saved successfully.");
                return true;
            }
            catch {
                // Handle errors and log them appropriately
                _logger.LogError("----- Create Collection History: An error occurred while creating and saving to the database.");
                return false;
            }
        }
    }
}