namespace LinkShortener.Backgrounds;

public class LinksCleaner : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _interval = TimeSpan.FromDays(1);

    public LinksCleaner(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using(IServiceScope scope = _scopeFactory.CreateScope())
            {
                IScopedProcessingService service = scope.ServiceProvider.GetService<IScopedProcessingService>()!;

                await service.DoWorkAsync(stoppingToken);
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}