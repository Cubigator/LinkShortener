using LinkShortenerDatabaseLib.Entities;

namespace LinkShortenerDatabaseLib.Repositories;

public interface IIPStatRepository
{
    Task AddRequestAsync(Request request);
    Task DeleteOldRequestsAsync();
    Task CalculateQueryStatisticsAsync();
    Task<IPStat?> GetStatisticsAsync(string ip);
}
