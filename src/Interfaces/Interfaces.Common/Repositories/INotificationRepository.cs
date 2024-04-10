using Data.AlphaBank;

namespace Interfaces.Common.Repositories {
    public interface INotificationRepository {
        public Task CreateAsync(Notification oNotification);
        public Task<string> GetMessageByIdAsync(int id);
        public Task SaveChangesAsync();
    }
}