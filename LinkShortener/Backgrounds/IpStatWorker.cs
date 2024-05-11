using LinkShortenerDatabaseLib.Repositories;

namespace LinkShortener.Backgrounds;

public class IpStatWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);
    public IpStatWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            using(IServiceScope scope = _scopeFactory.CreateScope())
            {
                IIPStatRepository repository = scope.ServiceProvider.GetService<IIPStatRepository>()!;

                await repository.CalculateQueryStatisticsAsync();
                await repository.DeleteOldRequestsAsync();
            }

            await Task.Delay(_interval);
        }
    }
}
