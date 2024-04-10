using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;
using Microsoft.Extensions.Logging;

namespace Service.Common {
    public class NotificationService(INotificationRepository notificationRepository,
                                     ILogger<NotificationService> logger) : INotificationService {

        private readonly INotificationRepository _notificationRepository
            = notificationRepository;

        private readonly ILogger<NotificationService> _logger = logger;

        public async Task<bool> Create(Notification oNotification) {
            try{
                _logger.LogInformation("----- Create Notification: Start the transaction to create a notification registry");
              
                await _notificationRepository.CreateAsync(oNotification);
                await _notificationRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Notification: Successfully completes saving to the transaction database.");

                // Return true to indicate successful creation.
                return true;
            } catch {
                _logger.LogError("--- Create Notification: An error occurred while creating and saving to the database.");

                //If there's an exception during the process, rollback the transaction and return false.
                return false;
            }
        }

        public async Task<string?> GetMessageById(int id) {
            try{
                //Return message
                return await _notificationRepository.GetMessageByIdAsync(id);
            }
            catch {
                return null;
            }
        }
    }
}