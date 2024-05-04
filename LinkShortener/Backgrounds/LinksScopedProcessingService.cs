using LinkShortenerDatabaseLib.Repositories;

namespace LinkShortener.Backgrounds;

public sealed class LinksScopedProcessingService : IScopedProcessingService
{
    private readonly ILinkRepository _linkRepository;
    private readonly TimeSpan _timeout;
    public LinksScopedProcessingService(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
        _timeout = TimeSpan.FromDays(1);
    }

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _linkRepository.DeleteExpiredLinksAsync();
            await _linkRepository.DeleteCompletedLinksAsync();

            await Task.Delay(_timeout, stoppingToken);
        }
    }
}
