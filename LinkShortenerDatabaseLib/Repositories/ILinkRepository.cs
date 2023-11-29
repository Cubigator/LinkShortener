using LinkShortenerDatabaseLib.Entities;

namespace LinkShortenerDatabaseLib.Repositories;

public interface ILinkRepository
{
    Task<Link?> GetLinkByIdAsync(int id);
    Task<Link?> GetByOldLinkAsync(string oldLink);
    Task<Link?> GetByNewLinkAsync(string newLink);
    Task DeleteExpiredLinksAsync();
    Task AddLinkTransitionByIdAsync(int id);
    Task AddLinkAsync(Link link);
    Task DeleteLinkAsync(Link link);
    Task DeleteLinkAsync(int id);
}
