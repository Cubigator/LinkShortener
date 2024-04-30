using LinkShortenerDatabaseLib.Repositories;

namespace LinkShortener.Backgrounds;

public sealed class LinksScopedProcessingService : IScopedProcessingService
{
    private readonly ILinkRepository _linkRepository;
    public LinksScopedProcessingService(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _linkRepository.DeleteExpiredLinksAsync();

            await Task.Delay(10_000, stoppingToken);
        }
    }
}
