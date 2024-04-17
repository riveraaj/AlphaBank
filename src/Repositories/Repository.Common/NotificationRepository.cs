using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class NotificationRepository(AlphaBankDbContext context) : INotificationRepository {

        private readonly AlphaBankDbContext _context = context;
        
        public async Task CreateAsync(Notification oNotification)
            => await _context.Notifications.AddAsync(oNotification);

        public async Task<string> GetMessageByIdAsync(int id) {
            try {
                var result = await _context.TypeNotifications.Where(x => x.Id == id)
                                               .Select(x => x.Message.Description)
                                               .SingleOrDefaultAsync()
                                                ?? " ";

                return result;
            }
            catch (SqlException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}