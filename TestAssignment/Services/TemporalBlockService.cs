
using TestAssignment.Data;

namespace TestAssignment.Services
{
    public class TemporalBlockService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                BlockedCounty.RemomveFromTemporalBlocks();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
