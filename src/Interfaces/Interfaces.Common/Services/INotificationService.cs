using Data.AlphaBank;

namespace Interfaces.Common.Services {
    public interface INotificationService {
        public Task<bool> Create(Notification oNotification);
        public Task<string?> GetMessageById(int id);
    }
}