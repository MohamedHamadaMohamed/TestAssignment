
using TestAssignment.Data;
using TestAssignment.Repository;
using TestAssignment.Repository.IRepository;

namespace TestAssignment.Services
{
    public class TemporalBlockService : BackgroundService
    {
       
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                InMemoryCollections.RemomveFromTemporalBlocks();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
