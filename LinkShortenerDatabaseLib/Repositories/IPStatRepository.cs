using LinkShortenerDatabaseLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortenerDatabaseLib.Repositories;

public class IPStatRepository : IIPStatRepository
{
    private readonly ApplicationContext _context;
    public IPStatRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task AddRequestAsync(Request request)
    {
        await _context.Requests.AddAsync(request);
        await _context.SaveChangesAsync();

        IPStat? ipStat = await _context.IPStats.FirstOrDefaultAsync(stat => stat.Ip == request.Ip);
        if (ipStat is null)
        {
            await _context.IPStats.AddAsync(new IPStat()
            {
                Ip = request.Ip,
                LastRequest = DateTime.UtcNow,
                RequestsCount = 1
            });
        }
        else
        {
            ipStat.LastRequest = DateTime.UtcNow;
            ipStat.RequestsCount++;
        }

        await _context.SaveChangesAsync();
    }

    public async Task CalculateQueryStatisticsAsync()
    {
        foreach(IPStat stat in _context.IPStats)
        {
            stat.RequestsCount = await _context.Requests.Where(request => request.Ip == stat.Ip).CountAsync();
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOldRequestsAsync()
    {
        IQueryable<Request> oldRequests = _context.Requests
            .Where(request => new TimeSpan(request.Time.Ticks) + new TimeSpan(0, 15, 0) < new TimeSpan(DateTime.UtcNow.Ticks));
        _context.Requests.RemoveRange(oldRequests);
        await _context.SaveChangesAsync();
    }
}
