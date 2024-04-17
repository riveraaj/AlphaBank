using Interfaces.ContinueLoans.Services;

namespace WebClient.Services {
    public class ContinueLoansService(IServiceScopeFactory serviceScopeFactory) : BackgroundService {

        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                using (var scope = _serviceScopeFactory.CreateScope()) {
                    var continueLoanService = scope.ServiceProvider.GetRequiredService<IContinueLoanService>();

                    await continueLoanService.PaymentBackgroundTask();
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Wait 1 minutes before running again.
            }
        }
    }
}