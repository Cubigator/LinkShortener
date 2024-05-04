using LinkShortenerDatabaseLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortenerDatabaseLib.Repositories;

public class LinkRepository : ILinkRepository
{
    private readonly ApplicationContext _context;
    public LinkRepository(ApplicationContext context)
    {
        _context = context; 
    }
    public async Task AddLinkAsync(Link link)
    {
        await _context.Links.AddAsync(link);
        await _context.SaveChangesAsync();
    }

    public async Task AddLinkTransitionByIdAsync(int id)
    {
        Link? link = await _context.Links
            .FirstOrDefaultAsync(x => x.Id == id);
        if (link != null)
        {
            link.NumberOfTransitions++;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCompletedLinksAsync()
    {
        IEnumerable<Link> links = _context.Links
            .Where(link => link.NumberOfTransitions > link.MaximumTransitionsCount);
        _context.Links.RemoveRange(links);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpiredLinksAsync()
    {
        IEnumerable<Link> links = _context.Links
            .Where(x => x.ExpirationDate < DateTime.UtcNow);
        _context.Links.RemoveRange(links);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLinkAsync(Link link)
    {
        _context.Links.Remove(link);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLinkAsync(int id)
    {
        _context.Links.Remove(new Link() { Id = id });
        await _context.SaveChangesAsync();
    }

    public async Task<Link?> GetByNewLinkAsync(string newLink)
    {
        return await _context.Links
            .FirstOrDefaultAsync(x => x.NewLink.Equals(newLink));

    }

    public async Task<Link?> GetByOldLinkAsync(string oldLink)
    {
        return await _context.Links
            .FirstOrDefaultAsync(x => x.OldLink.Equals(oldLink));
    }

    public async Task<Link?> GetLinkByIdAsync(int id)
    {
        return await _context.Links
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
